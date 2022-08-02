using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Mechanics;
using TD.Cameras;

namespace TD.Map
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Initialisation:")]

        [Tooltip("Map Prefab")][SerializeField] MapParent mapPrefab = null;
        MapParent mapParent;

        [Tooltip("Hex Prefab")][SerializeField] Hex hexPrefab = null;
        [SerializeField] float horizontalOffset = -0.13f;
        [SerializeField] float verticalOffset = -0.245f;
        //[SerializeField] HexMap hexMap = null;

        [Header("Map Attributes:")]
        [Range(12, 300)][SerializeField] private int horizontalMapSize = 12;
        [Range(12, 300)][SerializeField] private int verticalMapSize = 12;
        [HideInInspector] public Vector2Int mapSize = new Vector2Int(12,12);

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
        [SerializeField] HexDataBase hexDatabase;

        [Header("Rivers:")]
        [Tooltip("River Prefab")][SerializeField] RiverStream riverStreamPrefab = null;
        [SerializeField] HexAttributes minimalHighGroundType = null;
        [SerializeField] List<Hex> riverBeginingsList = new List<Hex>();
        [SerializeField] HexAttributes waterType = null;
        [Range(0f, 0.1f)][SerializeField] float relativeRiverAmount = 0.1f;
        [SerializeField] int initialFlowForce = 100;
        [Range(0.9f, 0.99f)][SerializeField] float flowReduction = 0.98f;
        [Range(0.25f, 0.75f)][SerializeField] float flowDevision = 0.5f;

        [Header("Trees:")]
        [Tooltip("River Prefab")][SerializeField] HexSpawnable treePrefab = null;
        [SerializeField] SpawnableDataBase TreeDataBase;
        [SerializeField] HexAttributes maxVegetationHexType = null;


        [Header("Buildings:")]
        [Tooltip("Building Prefab")][SerializeField] HexSpawnable buildingPrefab = null;
        [SerializeField] EncounterDataBase buildingDataBase;

        [Header("Units:")]
        [Tooltip("Unit Prefab")][SerializeField] HexSpawnable unitPrefab = null;
        [SerializeField] EncounterDataBase unitDataBase;

        void Start()
        {

        }

        #region Haxagonal Map

        public void CreateMapParent()
        {
            mapParent = Instantiate(mapPrefab, transform.position, Quaternion.identity);
            //TODO: Name the map
        }

        public void BuildHexMap()
        {
            mapSize.x = horizontalMapSize;
            mapSize.y = verticalMapSize;

            mapParent.hexMap = new HexMap(mapSize);

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

                    mapParent.hexMap.SetHexToMap(hex);
                }
            }

            mapParent.SetMapSize(mapSize);
            mapParent.SetLastHexPos(mapParent.hexMap.GetLastHexFromMap(mapSize).transform.position);
        }

        private void FixPosition(Hex hex)
        {

            if (hex.GetHexCoordinates().y%2 != 0)
            {
                hex.transform.position = new Vector3(hex.transform.position.x + (hex.GetHexCoordinates().x * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetHexCoordinates().y * verticalOffset);
            }
            else
            {
                hex.transform.position = new Vector3(hex.transform.position.x + ((hex.transform.localScale.x + horizontalOffset)/2) + (hex.GetHexCoordinates().x * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetHexCoordinates().y * verticalOffset);
            }

        }

        public void SetHexNeighbours()
        {
            int dirLength = System.Enum.GetValues(typeof(Direction)).Length;

            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                Direction currentDirection = 0;
                List<Hex> hexNeighbours = new List<Hex>();

                for (int dir = 0; dir < dirLength; dir++)
                {
                    currentDirection = (Direction)dir;

                    Hex neighbour = mapParent.hexMap.GetNeighbour(hex.GetHexCoordinates(), currentDirection, mapSize);

                    if (neighbour != null)
                    {
                        hexNeighbours.Add(neighbour);

                    }

                }

                hex.SetNeighbourHexes(hexNeighbours);
            }
        }

        #endregion

        #region Elevation

        private void SetupMapPattern()
        {
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
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                hex.SetHexPatternElevation(MapCalculator.CalculateElevation(currentPattern,
                                                                            mapSize.x,
                                                                            mapSize.y,
                                                                            hex.GetHexCoordinates().x,
                                                                            hex.GetHexCoordinates().y));
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

            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                hex.SetHexNoiseElevation(noiseMap[hex.GetHexCoordinates().x, hex.GetHexCoordinates().y]);
            }
        }

        private void SetHexElevationMap()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                hex.SetElevation();
            }
        }

        #endregion

        #region Hexes

        private void SetHexAttributes()
        {
            foreach(Hex hex in mapParent.hexMap.GetHexMap())
            {
                foreach (HexAttributes hexType in hexDatabase.hexList)
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
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                hex.SetHexSprite();
            }
        }

        private void SetBlackAndWhiteHeightMap()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                hex.SetHexColor();
            }
        }

        #endregion

        #region Rivers

        private void BuildRiverBeginingsList()
        {
            List<Hex> highGroundList = new List<Hex>();

            riverBeginingsList.Clear();

            foreach (Hex hex in mapParent.hexMap.GetHexMap())
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
                //hex.GetComponentInChildren<SpriteRenderer>().color = new Color(0,1,0) * hex.GetElevation(); // DEBUG

                GenerateOneRiver(hex, 0, initialFlowForce);
            }
        }

        private List<Hex> FindLowestNieghbours(Hex hex)
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

            List<Hex> lowestNeighbours = FindLowestNieghbours(hex);


            hex.SetHasRiverStream(true);

            foreach (Hex neighbour in lowestNeighbours)
            {
                int roll = Random.Range(step, initialFlowForce);

                if (roll >= flowForce)
                {
                    continue;
                }

                RiverStream stream = Instantiate(riverStreamPrefab, hex.transform.position, hex.transform.rotation, hex.transform);
                hex.SetRiverStream(stream);

                stream.transform.LookAt(neighbour.transform.position, Vector3.up);
                stream.transform.Rotate(new Vector3(0, 270, 0));


                float deltaFlow = flowForce * flowReduction;
                flowForce *= flowDevision;

                if (step == 0)
                {
                    stream.SetAsFirst();
                }

                if (neighbour.GetElevation() <= waterType.GetTypeElevation())
                {
                    if (hex.GetRiverStream() != null)
                    {
                        hex.GetRiverStream().SetAsLast();
                    }
                }

                stream.SetSprite();

                GenerateOneRiver(neighbour, step+1, deltaFlow);
            }
        }

        #endregion

        #region Trees

        private void SetHexVegetation()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                if (hex.GetElevation() <= waterType.GetTypeElevation())
                {
                    continue;
                }

                hex.SetVegetationAmount(MapCalculator.CalculateVegetationAmount(hex.GetElevation(), maxVegetationHexType.GetTypeElevation()));
            }
        }


        private void GenerateVegetation()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                if (hex.GetElevation() <= waterType.GetTypeElevation())
                {
                    continue;
                }

                hex.GetSpawner().SpawnObjects(treePrefab.gameObject, hex.GetVegetationAmount(),treePrefab.GetSpread(),treePrefab.GetBoxOverlap(), treePrefab.GetLayer(), hex.gameObject);
            }
        }

        private void SetTreeAttributes()
        {
            HexSpawnable[] allTrees = FindObjectsOfType<HexSpawnable>();

            //HexSpawnable[] allTrees = GameObject.FindGameObjectsWithTag("Tree");

            foreach (HexSpawnable tree in allTrees)
            {
                if (tree.gameObject.CompareTag("Tree"))
                {
                    tree.SetSpawnableAttributes(tree.gameObject.GetComponentInParent<Hex>().GetHexAttributes().GetRandomTreeType());
                    tree.SetSprites();
                }
            }
        }

        #endregion

        #region Buildings

        private void GenerateBuildings()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                if (hex.GetElevation() <= waterType.GetTypeElevation())
                {
                    continue;
                }

                int roll = RollDice.Roll(20);

                if (roll > 4) // TODO: Make it dynamic depends on difficulty and number of players
                {
                    continue;
                }

                hex.GetSpawner().SpawnObjects(buildingPrefab.gameObject, 1, buildingPrefab.GetSpread(), buildingPrefab.GetBoxOverlap(), buildingPrefab.GetLayer(), hex.gameObject);
                hex.GetSpawner().DeactivateSpawner();
            }
        }

        private void SetBuildingEncounter()
        {
            Encounter[] buildingEncounters = FindObjectsOfType<Encounter>();

            foreach (Encounter building in buildingEncounters)
            {
                if (building.gameObject.CompareTag("Building"))
                {
                    building.SetEncounterAttributes(buildingDataBase.encounterList[Random.Range(0, buildingDataBase.encounterList.Count)]);

                    HexSpawnable buildingSpawnable = building.gameObject.GetComponent<HexSpawnable>();

                    buildingSpawnable.SetSpawnableAttributes(building.GetEncounterAttributes().GetSpawnablAttributes());
                    buildingSpawnable.SetSprites();

                    building.SetEncounterStats();
                }
            }
        }

        #endregion

        #region Units

        private void GenerateUnits()
        {
            foreach (Hex hex in mapParent.hexMap.GetHexMap())
            {
                if (hex.GetElevation() <= waterType.GetTypeElevation())
                {
                    continue;
                }

                int roll = RollDice.Roll(20);

                if (roll > 8) // TODO: Make it dynamic depends on difficulty and number of players
                {
                    continue;
                }

                hex.GetSpawner().SpawnObjects(unitPrefab.gameObject, 1, unitPrefab.GetSpread(), unitPrefab.GetBoxOverlap(), unitPrefab.GetLayer(), hex.gameObject);
                hex.GetSpawner().DeactivateSpawner();
            }
        }

        private void SetUnitEncounter()
        {
            Encounter[] unitEncounters = FindObjectsOfType<Encounter>();

            foreach (Encounter unit in unitEncounters)
            {
                if (unit.gameObject.CompareTag("Unit"))
                {
                    unit.SetEncounterAttributes(unitDataBase.encounterList[Random.Range(0, unitDataBase.encounterList.Count)]);

                    HexSpawnable unitSpawnable = unit.gameObject.GetComponent<HexSpawnable>();

                    unitSpawnable.SetSpawnableAttributes(unit.GetEncounterAttributes().GetSpawnablAttributes());
                    unitSpawnable.SetSprites();

                    unit.SetEncounterStats();

                    int roll = RollDice.Roll(2);

                    if (roll > 1) // flip
                    {
                        unitSpawnable.GetPrimarySprite().flipX = true;
                    }
                }
            }
        }

        #endregion


        #region Clearing

        private void OrderSpawnableSprites()
        {
            HexSpawnable[] allHexSpawnables = FindObjectsOfType<HexSpawnable>();

            foreach (HexSpawnable hexSpawnable in allHexSpawnables)
            {
                hexSpawnable.GetPrimarySprite().sortingLayerName = "Spawnable";
                hexSpawnable.GetPrimarySprite().sortingOrder = Mathf.RoundToInt(hexSpawnable.gameObject.transform.position.z * -100);

                hexSpawnable.GetSecondarySprite().sortingLayerName = "Spawnable";
                hexSpawnable.GetSecondarySprite().sortingOrder = Mathf.RoundToInt(hexSpawnable.gameObject.transform.position.z * -100);
            }    
        }

        private void ClearGenerationElements()
        {
            HexAreaSpawner[] hexAreaSpawners = FindObjectsOfType<HexAreaSpawner>();

            foreach (HexAreaSpawner areaSpawner in hexAreaSpawners)
            {
                Destroy(areaSpawner);
            }
 
            RiverStream[] streams = FindObjectsOfType<RiverStream>();

            foreach (RiverStream stream in streams)
            {
                Destroy(stream);
            }

            HexSpawnable[] allTrees = FindObjectsOfType<HexSpawnable>();

            foreach (HexSpawnable tree in allTrees)
            {
                Destroy(tree);
            }


        }

        #endregion

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
                foreach (Hex hex in mapParent.hexMap.GetHexMap())
                {
                    if ((hex.GetHexCoordinates().x >= area.GetStartPosition().x) &&
                        (hex.GetHexCoordinates().x <= area.GetEndPosition().x) &&
                        (hex.GetHexCoordinates().y >= area.GetStartPosition().y) &&
                        (hex.GetHexCoordinates().y <= area.GetEndPosition().y))
                    {
                        area.AddToHexList(hex);
                    }

                }
            }
        }

        #region Public Functions

        public void SetSize(int sizeToSet)
        {
            horizontalMapSize = sizeToSet;
            verticalMapSize = sizeToSet;
        }

        public void SetPattern(int typeToSet)
        {
            currentPattern = (PatternType)typeToSet;
        }

        public void SetRrandomSeed(bool isRandom)
        {
            randomSeed = isRandom;
        }

        public void SetSeed(int seedToSet)
        {
            seed = seedToSet;
        }

        public int GetSeed()
        {
            return seed;
        }

        public void GenerateMap()
        {
            Assets.Scripts.Profiler profiler = new Assets.Scripts.Profiler("CreateMapParent");

            CreateMapParent();

            profiler.measureDeltaTime("PlaceHexes");

            BuildHexMap();

            profiler.measureDeltaTime("SetHexNeighbours");

            SetHexNeighbours();

            profiler.measureDeltaTime("SetupMapPattern");

            SetupMapPattern();

            profiler.measureDeltaTime("BuildPatternElevationMap");

            BuildPatternElevationMap();

            profiler.measureDeltaTime("BuildNoiseElevationMap");

            BuildNoiseElevationMap();

            profiler.measureDeltaTime("SetHexElevationMap");

            SetHexElevationMap();

            profiler.measureDeltaTime("SetHexAttributes");

            SetHexAttributes();

            profiler.measureDeltaTime("SetHexSprites");

            SetHexSprites();

            //SetBlackAndWhiteHeightMap();

            profiler.measureDeltaTime("BuildRiverBeginingsList");

            BuildRiverBeginingsList();

            profiler.measureDeltaTime("GenerateRivers");

            GenerateRivers();

            profiler.measureDeltaTime("SetHexVegetation");

            SetHexVegetation();

            profiler.measureDeltaTime("GenerateVegetation");


            GenerateVegetation();

            profiler.measureDeltaTime("SetTreeAttributes");

            SetTreeAttributes();

            GenerateBuildings();

            SetBuildingEncounter();

            GenerateUnits();

            SetUnitEncounter();

            //TODO: order all sptites in the hex using z position (top down) 

            //SetupAreas();
            //DevideAllHexesToCardinalAreas();

            OrderSpawnableSprites();

            ClearGenerationElements();
        }

        #endregion

    }
}


