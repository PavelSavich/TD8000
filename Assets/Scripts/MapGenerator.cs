using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Mechanics;

namespace TD.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Initialisation:")]

        [Tooltip("Map Prefab")]
        [SerializeField] Map mapPrefab = null;
        Map mapParent;

        [Tooltip("Hex Prefab")]
        [SerializeField] Hex hexPrefab = null;
        [SerializeField] float horizontalOffset = -0.13f;
        [SerializeField] float verticalOffset = -0.245f;
        [SerializeField] HexMap hexMap = null;

        [Tooltip("River Prefab")]
        [SerializeField] RiverStream riverStreamPrefab = null;
        [SerializeField] HexAttributes minimalHighGroundType = null;
        [SerializeField] List<Hex> riverBeginingsList = new List<Hex>();
        [SerializeField] HexAttributes waterType = null;
        [Range(0f,0.1f)][SerializeField] float relativeRiverAmount = 0.1f;
        [SerializeField] int initialFlowForce = 100;
        [Range(0.9f,0.99f)][SerializeField] float flowReduction = 0.98f;
        [Range(0.25f, 0.75f)][SerializeField] float flowDevision = 0.5f;

        [Header("Map Attributes:")]
        [Range(12, 300)][SerializeField] private int horizontalMapSize = 12;
        [Range(12, 300)][SerializeField] private int verticalMapSize = 12;
        public Vector2Int mapSize = new Vector2Int(12,12);

        [Header("Map Pattern:")]
        [SerializeField] PatternAttributes[] availiblePatterns;
        PatternAttributes patternAttributes = null;
        [SerializeField] PatternType currentPattern = 0;
        [SerializeField] public List<AreaInMap> cardinalsInMap = new List<AreaInMap>();

        [Header("Noise")]
        [Range(0.0001f, 50f)] [SerializeField] float noiseScale = 0.0001f;
        [Range(0,25)][SerializeField] int octaves = 0;
        [Range(0f, 1f)][SerializeField] float persistance = 0f;
        [Range(1f,25f)][SerializeField] float lacunarity = 1f;
        [SerializeField] int seed = 0;
        [SerializeField] bool randomSeed = false;
        [SerializeField] Vector2 offset = new Vector2();
        [SerializeField] bool randomOffset = false;

        [Header("Map Hex Types:")]
        [SerializeField] HexAttributes[] availibleHexTypes;

        void Start()
        {
            var p = new Assets.Scripts.Profiler("CreateMapParent");

            CreateMapParent();

            p.measureDeltaTime("PlaceHexes");

            BuildHexMap();

            p.measureDeltaTime("SetHexNeighbours");

            SetHexNeighbours();

            p.measureDeltaTime("SetupMapPattern");

            SetupMapPattern();

            p.measureDeltaTime("BuildPatternElevationMap");

            BuildPatternElevationMap();

            p.measureDeltaTime("BuildNoiseElevationMap");

            BuildNoiseElevationMap();

            p.measureDeltaTime("SetHexElevationMap");

            SetHexElevationMap();

            p.measureDeltaTime("SetHexAttributes");

            SetHexAttributes();

            p.measureDeltaTime("SetHexSprites");

            SetHexSprites();

            BuildRiverBeginingsList();

            GenerateRivers();

            //SetupAreas();
            //DevideAllHexesToCardinalAreas();
        }

        public void CreateMapParent()
        {
            mapParent = Instantiate(mapPrefab, transform.position, Quaternion.identity, gameObject.transform);
            //TODO: Name the map
        }

        public void BuildHexMap()
        {
            mapSize.x = horizontalMapSize;
            mapSize.y = verticalMapSize;

            hexMap = new HexMap(mapSize);

            for (int xPosition = 0; xPosition < mapSize.x; xPosition ++)
            {
                for (int yPosition = 0; yPosition < mapSize.y; yPosition++)
                {
                    Hex hex = Instantiate(hexPrefab, new Vector3(xPosition*transform.localScale.x,
                                                                 0,
                                                                 yPosition*transform.localScale.y),
                                                                 hexPrefab.transform.rotation, mapParent.transform);

                    hex.SetHexCoordinates(new Vector2Int(xPosition,yPosition));

                    hex.SetHexName($"{xPosition},{yPosition}");

                    FixPosition(hex);

                    hexMap.SetHexToMap(hex);
                }
            }
        }

        private void FixPosition(Hex hex)
        {

            if (hex.GetHexCoordinate().y%2 != 0)
            {
                hex.transform.position = new Vector3(hex.transform.position.x + (hex.GetHexCoordinate().x * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetHexCoordinate().y * verticalOffset);
            }
            else
            {
                hex.transform.position = new Vector3(hex.transform.position.x + ((hex.transform.localScale.x + horizontalOffset)/2) + (hex.GetHexCoordinate().x * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetHexCoordinate().y * verticalOffset);
            }

        }

        public void SetHexNeighbours()
        {
            int dirLength = System.Enum.GetValues(typeof(Direction)).Length;

            foreach (Hex hex in hexMap.GetHexMap())
            {
                Direction currentDirection = 0;
                List<Hex> hexNeighbours = new List<Hex>();

                for (int dir = 0; dir < dirLength; dir++)
                {
                    currentDirection = (Direction)dir;

                    Hex neighbour = hexMap.GetNeighbour(hex.GetHexCoordinate(), currentDirection, mapSize);

                    if (neighbour != null)
                    {
                        hexNeighbours.Add(neighbour);

                    }

                }

                hex.SetNeighbourHexes(hexNeighbours);
            }
        }

        private void SetupMapPattern()
        {

            if (currentPattern == PatternType.none)
            {
                Debug.LogError("Pattern is Missing");
                return;
            }

            foreach (PatternAttributes availiblePattern in availiblePatterns)
            {
                if (availiblePattern.GetPattern() == currentPattern)
                {
                    patternAttributes = availiblePattern;
                    break;
                }
            }

            if (patternAttributes == null) { return; }
        }

        private void BuildPatternElevationMap()
        {
            foreach (Hex hex in hexMap.GetHexMap())
            {
                hex.SetHexPatternElevation(MapCalculator.CalculateElevation(currentPattern,mapSize.x,mapSize.y,hex.GetHexCoordinate().x, hex.GetHexCoordinate().y));
            }
        }

        private void BuildNoiseElevationMap()
        {
            if (randomSeed)
            {
                seed = Random.Range(int.MinValue,int.MaxValue); 
            }

            if (randomOffset)
            {
                offset = new Vector2(Random.Range(-mapSize.x,mapSize.x),Random.Range(-mapSize.y,mapSize.y));
            }

            float[,] noiseMap = MapCalculator.GenerateNoiseMap(mapSize.x, mapSize.y, seed, noiseScale, octaves, persistance, lacunarity, offset);

            foreach (Hex hex in hexMap.GetHexMap())
            {
                hex.SetHexNoiseElevation(noiseMap[hex.GetHexCoordinate().x, hex.GetHexCoordinate().y]);
            }
        }

        private void SetHexElevationMap()
        {
            foreach (Hex hex in hexMap.GetHexMap())
            {
                hex.SetElevation();
            }
        }

        private void SetHexAttributes()
        {
            foreach(Hex hex in hexMap.GetHexMap())
            {
                foreach (HexAttributes hexType in availibleHexTypes)
                {
                    if (hex.GetElevation() <= hexType.GetTypeElevation())
                    {
                        hex.SetHexAttributes(hexType);
                        break;
                    }
                }
            }
        }

        private void SetHexSprites()
        {
            foreach (Hex hex in hexMap.GetHexMap())
            {
                hex.SetHexSprite();
            }
        }

        private void SetBlackAndWhiteHeightMap()
        {
            foreach (Hex hex in hexMap.GetHexMap())
            {
                hex.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1) * hex.GetElevation();
            }
        }

        private void BuildRiverBeginingsList()
        {
            List<Hex> highGroundList = new List<Hex>();


            foreach (Hex hex in hexMap.GetHexMap())
            {
                if (hex.GetElevation() >= minimalHighGroundType.GetTypeElevation())
                {
                    highGroundList.Add(hex);
                }
            }

            for (int randomHexCount = 0; randomHexCount < Mathf.RoundToInt(highGroundList.Count* relativeRiverAmount); randomHexCount++)
            {
                int randomHexIndex = Random.Range(0, highGroundList.Count);

                if (riverBeginingsList.Contains(highGroundList[randomHexIndex]))
                {
                    randomHexCount--;
                }
                else
                {
                    riverBeginingsList.Add(highGroundList[randomHexIndex]);
                }
            }
        }

        private void GenerateRivers()
        {
            foreach (Hex hex in riverBeginingsList)
            {
                hex.GetComponentInChildren<SpriteRenderer>().color = new Color(0,1,0) * hex.GetElevation();

                GenerateOneRiver(hex, 0, initialFlowForce);
            }
        }

        private List<Hex> FindLowerNieghbours(Hex hex)
        {
            float currentMaxElevation = hex.GetElevation();

            List<Hex> lowestNeighbours = new List<Hex>();

            foreach (Hex neighbour in hex.GetNeighbourHexes())
            {
                if (neighbour.GetElevation() < currentMaxElevation)
                {
                    if (neighbour.GetHasRiverStream())
                    {
                        continue;
                    }

                    lowestNeighbours.Add(neighbour);
                }
            }

            lowestNeighbours.Sort((a, b) => a.GetElevation().CompareTo(b.GetElevation()));

            return lowestNeighbours;
        }

        private void GenerateOneRiver(Hex hex, int step, float flowForce)
        {
            if (hex.GetElevation() <= waterType.GetTypeElevation())
            {
                return;
            }

            List<Hex> lowestNeighbours = FindLowerNieghbours(hex);

            hex.SetRiverStream(true);

            foreach (Hex neighbour in lowestNeighbours)
            {
                int roll = Random.Range(step, initialFlowForce);

                if (roll >= flowForce)
                {
                    continue;
                }

                float delta = flowForce * flowReduction;
                flowForce *= flowDevision;

                RiverStream stream = Instantiate(riverStreamPrefab, hex.transform.position, hex.transform.rotation, hex.transform);

                stream.transform.LookAt(neighbour.transform.position, Vector3.up);
                stream.transform.Rotate(new Vector3(0, 270, 0));

                GenerateOneRiver(neighbour, step+1, delta);
            }
        }

        private void SetupAreas()
        {
            foreach (AreaInMap area in cardinalsInMap)
            {
                area.SetStartPosition(MapCalculator.CalculteCardinalStartPositions(area.GetCardinalArea(), mapSize.x, mapSize.y));
                area.SetEndPosition(MapCalculator.CalculteCardinalEndPositions(area.GetCardinalArea(), mapSize.x, mapSize.y));
            }
        }

        private void DevideAllHexesToCardinalAreas()
        {
            foreach (AreaInMap area in cardinalsInMap)
            {
                foreach (Hex hex in hexMap.GetHexMap())
                {
                    if ((hex.GetHexCoordinate().x >= area.GetStartPosition().x) &&
                        (hex.GetHexCoordinate().x <= area.GetEndPosition().x) &&
                        (hex.GetHexCoordinate().y >= area.GetStartPosition().y) &&
                        (hex.GetHexCoordinate().y <= area.GetEndPosition().y))
                    {
                        area.AddToHexList(hex);
                    }

                }
            }
        }
    }
}


