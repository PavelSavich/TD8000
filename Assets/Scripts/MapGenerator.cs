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
        [SerializeField] List<Hex> allHexes = new List<Hex>();

        [Header("Map Attributes:")]
        [Range(12,300)][SerializeField] public int horizontalSize = 12;
        [Range(12, 300)][SerializeField] public int verticalSize = 12;

        [Header("Map Pattern:")]
        [SerializeField] PatternAttributes[] availiblePatterns;
        PatternAttributes patternAttributes = null;
        [SerializeField] PatternType currentPattern = 0;
        [SerializeField] public List<AreaInMap> cardinalsInMap = new List<AreaInMap>();

        [Header("Map Hex Types:")]
        [SerializeField] HexAttributes[] availibleHexTypes;

        void Start()
        {
            //var time = Time.realtimeSinceStartup;
            CreateMapParent();


            PlaceHexes();
            SetHexNeighbours();


            SetupMapPattern();
            BuildPatternWithMathFunction();

            Normalize();
            SetHexSprites();

            //print($"CreateMapParent {(Time.realtimeSinceStartup - time).ToString("f6")}");

            //SetupAreas();
            //DevideAllHexesToCardinalAreas();
        }

        public List<Hex> GetAllHexes()
        {
            return allHexes;
        }

        public void CreateMapParent()
        {
            mapParent = Instantiate(mapPrefab, transform.position, Quaternion.identity, gameObject.transform);
            //TODO: Name the map
        }

        public void PlaceHexes()
        {
            for (int xPosition = 1; xPosition <= horizontalSize; xPosition ++)
            {
                for (int yPosition = 1; yPosition <= verticalSize; yPosition++)
                {
                    Hex hex = Instantiate(hexPrefab, new Vector3(xPosition*transform.localScale.x, 0, yPosition*transform.localScale.y), hexPrefab.transform.rotation, mapParent.transform);
                    hex.SetX(xPosition);
                    hex.SetY(yPosition);

                    hex.SetHexName($"{xPosition},{yPosition}");

                    FixPosition(hex);

                    allHexes.Add(hex);
                }
            }
        }

        private void FixPosition(Hex hex)
        {
            if (hex.GetY()%2 != 0)
            {
                hex.transform.position = new Vector3(hex.transform.position.x + (hex.GetX() * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetY() * verticalOffset);
            }
            else
            {
                hex.transform.position = new Vector3(hex.transform.position.x + ((hex.transform.localScale.x + horizontalOffset)/2) + (hex.GetX() * horizontalOffset),
                                                     0,
                                                     hex.transform.position.z + hex.GetY() * verticalOffset);
            }

        }

        public void SetHexNeighbours()
        {
            foreach (Hex hex in allHexes)
            {
                hex.SetNeighbourHexes(FindNeighbours(hex));
            }
        }

        public List<Hex> FindNeighbours(Hex hex)
        {
            List<Hex> hexNeighbours = new List<Hex>();

            if (hex.GetY() % 2 == 0) // EVEN ROW (Offset Row)
            {
                foreach (Hex hexInMap in allHexes)
                {
                    if ((hexInMap.GetX() == hex.GetX() + 1) && (hexInMap.GetY() == hex.GetY()) ||
                        (hexInMap.GetX() == hex.GetX() + 1) && (hexInMap.GetY() == hex.GetY() - 1) ||
                        (hexInMap.GetX() == hex.GetX()) && (hexInMap.GetY() == hex.GetY() - 1) ||
                        (hexInMap.GetX() == hex.GetX() - 1) && (hexInMap.GetY() == hex.GetY()) ||
                        (hexInMap.GetX() == hex.GetX()) && (hexInMap.GetY() == hex.GetY() + 1) ||
                        (hexInMap.GetX() == hex.GetX() + 1) && (hexInMap.GetY() == hex.GetY() + 1)
                        )
                    {
                        if (!hexNeighbours.Contains(hexInMap))
                        {
                            hexNeighbours.Add(hexInMap);
                        }
                    }
                }
            }
            else if (hex.GetY() % 2 != 0) // ODD ROW
            {
                foreach (Hex hexInMap in allHexes)
                {
                    if ((hexInMap.GetX() == hex.GetX() + 1) && (hexInMap.GetY() == hex.GetY()) ||
                        (hexInMap.GetX() == hex.GetX()) && (hexInMap.GetY() == hex.GetY() - 1) ||
                        (hexInMap.GetX() == hex.GetX() - 1) && (hexInMap.GetY() == hex.GetY() - 1) ||
                        (hexInMap.GetX() == hex.GetX() - 1) && (hexInMap.GetY() == hex.GetY()) ||
                        (hexInMap.GetX() == hex.GetX() - 1) && (hexInMap.GetY() == hex.GetY() + 1) ||
                        (hexInMap.GetX() == hex.GetX()) && (hexInMap.GetY() == hex.GetY() + 1)
                        )
                    {
                        if (!hexNeighbours.Contains(hexInMap))
                        {
                            hexNeighbours.Add(hexInMap);
                        }
                    }
                }
            }

            return hexNeighbours;
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

        private void BuildPatternWithMathFunction()
        {
            foreach (Hex hex in allHexes)
            {
                hex.SetElevation(MapCalculator.CalculateElevation(currentPattern,horizontalSize,verticalSize,hex.GetX(),hex.GetY()));

                List<HexAttributes> potentialAttributesForHex = new List<HexAttributes>();

                foreach (HexAttributes hexType in availibleHexTypes)
                {
                    float noiseElevation = Random.Range(hexType.GetMinTypeElevation(), hexType.GetMaxTypeElevation());

                    if ((hex.GetElevation() + noiseElevation) / 2 >= hexType.GetMinTypeElevation() && (hex.GetElevation() + noiseElevation) / 2 <= hexType.GetMaxTypeElevation())
                    {
                        potentialAttributesForHex.Add(hexType);
                    }
                }

                hex.SetHexAttributes(potentialAttributesForHex[Random.Range(0 ,potentialAttributesForHex.Count)]);
                potentialAttributesForHex.Clear();
            }
        }

        private void Normalize()
        {

        }

        private void SetHexSprites()
        {
            foreach (Hex hex in allHexes)
            {
                if (hex.GetHexAttributes() != null)
                {
                    hex.SetHexSprite();
                }
            }
        }

        private void SetupAreas()
        {
            foreach (AreaInMap area in cardinalsInMap)
            {
                area.SetStartPosition(MapCalculator.CalculteCardinalStartPositions(area.GetCardinalArea(), horizontalSize, verticalSize));
                area.SetEndPosition(MapCalculator.CalculteCardinalEndPositions(area.GetCardinalArea(), horizontalSize, verticalSize));
            }
        }

        private void DevideAllHexesToCardinalAreas()
        {
            foreach (AreaInMap area in cardinalsInMap)
            {
                foreach (Hex hex in allHexes)
                {
                    if ((hex.GetX() >= area.GetStartPosition().x) &&
                        (hex.GetX() <= area.GetEndPosition().x) &&
                        (hex.GetY() >= area.GetStartPosition().y) &&
                        (hex.GetY() <= area.GetEndPosition().y))
                    {
                        area.AddToHexList(hex);
                    }

                }
            }
        }
    }
}


