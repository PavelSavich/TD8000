using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class MapCalculator
    {

        public static Vector2 CalculteCardinalStartPositions(CardinalAreas area, int horizontalSize, int verticalSize)
        {
            int horizontalResult = 0;
            int verticalResult = 0;

            switch (area)
            {
                case CardinalAreas.southWest:
                    horizontalResult = 0;
                    verticalResult = 0;
                    break;

                case CardinalAreas.south:
                    horizontalResult = 0;
                    verticalResult = 0;
                    break;

                case CardinalAreas.southEast:
                    horizontalResult = Mathf.RoundToInt((horizontalSize / 3) * 2);
                    verticalResult = 0;
                    break;

                case CardinalAreas.west:
                    horizontalResult = 0;
                    verticalResult = 0;
                    break;

                case CardinalAreas.center:
                    horizontalResult = Mathf.RoundToInt(horizontalSize / 3);
                    verticalResult = Mathf.RoundToInt(verticalSize / 3);
                    break;

                case CardinalAreas.east:
                    horizontalResult = Mathf.RoundToInt((horizontalSize / 3) * 2);
                    verticalResult = 0;
                    break;

                case CardinalAreas.nortWest:
                    horizontalResult = 0;
                    verticalResult = Mathf.RoundToInt((verticalSize / 3) * 2);
                    break;

                case CardinalAreas.north:
                    horizontalResult = 0;
                    verticalResult = Mathf.RoundToInt((verticalSize / 3) * 2);
                    break;

                case CardinalAreas.nortEast:
                    horizontalResult = Mathf.RoundToInt((horizontalSize / 3) * 2);
                    verticalResult = Mathf.RoundToInt((verticalSize / 3) * 2);
                    break;

                case CardinalAreas.global:
                    horizontalResult = 0;
                    verticalResult = 0;
                    break;
            }

            return new Vector2(horizontalResult, verticalResult);
        }

        public static Vector2 CalculteCardinalEndPositions(CardinalAreas area, int horizontalSize, int verticalSize)
        {
            int horizontalResult = 0;
            int verticalResult = 0;

            switch (area)
            {
                case CardinalAreas.southWest:
                    horizontalResult = Mathf.RoundToInt(horizontalSize / 3);
                    verticalResult = Mathf.RoundToInt(verticalSize / 3);
                    break;

                case CardinalAreas.south:
                    horizontalResult = horizontalSize;
                    verticalResult = Mathf.RoundToInt(verticalSize / 3);
                    break;

                case CardinalAreas.southEast:
                    horizontalResult = horizontalSize;
                    verticalResult = Mathf.RoundToInt(verticalSize / 3);
                    break;

                case CardinalAreas.west:
                    horizontalResult = Mathf.RoundToInt(horizontalSize / 3);
                    verticalResult = verticalSize;
                    break;

                case CardinalAreas.center:
                    horizontalResult = Mathf.RoundToInt((horizontalSize / 3)*2);
                    verticalResult = Mathf.RoundToInt((verticalSize / 3)*2);
                    break;

                case CardinalAreas.east:
                    horizontalResult = horizontalSize;
                    verticalResult = verticalSize;
                    break;

                case CardinalAreas.nortWest:
                    horizontalResult = Mathf.RoundToInt(horizontalSize / 3);
                    verticalResult = verticalSize;
                    break;

                case CardinalAreas.north:
                    horizontalResult = horizontalSize;
                    verticalResult = verticalSize;
                    break;

                case CardinalAreas.nortEast:
                    horizontalResult = horizontalSize;
                    verticalResult = verticalSize;
                    break;

                case CardinalAreas.global:
                    horizontalResult = horizontalSize;
                    verticalResult = verticalSize;
                    break;
            }

            return new Vector2(horizontalResult, verticalResult);
        }

        static public Hex GetInitialHex(AreaInMap area, InitialHexFrom initialHexFrom) 
        {
            Hex initialHex = null;

            switch (initialHexFrom)
            {
                case InitialHexFrom.center:

                    Vector2 areaCenter = new Vector2(Mathf.RoundToInt((area.GetEndPosition().x + area.GetStartPosition().x) / 2), Mathf.RoundToInt((area.GetEndPosition().y + area.GetStartPosition().y) / 2));

                    int randomXCenter = Mathf.RoundToInt(Random.Range(
                                        Mathf.Max(area.GetStartPosition().x, areaCenter.x - (areaCenter.x / 20)),
                                        Mathf.Min(area.GetEndPosition().x, areaCenter.x + (areaCenter.x / 20))));

                    int randomYCenter = Mathf.RoundToInt(Random.Range(
                    Mathf.Max(area.GetStartPosition().y, areaCenter.y - (areaCenter.y / 20)),
                    Mathf.Min(area.GetEndPosition().y, areaCenter.y + (areaCenter.y / 20))));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetHexCoordinates().x == randomXCenter && hex.GetHexCoordinates().y == randomYCenter)
                        {
                            initialHex = hex;
                        }
                    }

                    break;

                case InitialHexFrom.random:

                    initialHex = area.GetHexList()[Random.Range(1, area.GetHexList().Count)];

                    break;

                case InitialHexFrom.top:

                    int randomXTop = Mathf.RoundToInt(Random.Range(area.GetStartPosition().x, area.GetEndPosition().x));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetHexCoordinates().x == randomXTop && hex.GetHexCoordinates().y == area.GetEndPosition().y)
                        {
                            initialHex = hex;
                        }
                    }

                    break;

                case InitialHexFrom.bottom:

                    int randomXBottom = Mathf.RoundToInt(Random.Range(area.GetStartPosition().x, area.GetEndPosition().x));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetHexCoordinates().x == randomXBottom && hex.GetHexCoordinates().y == area.GetStartPosition().y)
                        {
                            initialHex = hex;
                        }
                    }

                    break;

                case InitialHexFrom.right:

                    int randomYRight = Mathf.RoundToInt(Random.Range(area.GetStartPosition().y, area.GetEndPosition().y));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetHexCoordinates().x == area.GetEndPosition().x && hex.GetHexCoordinates().y == randomYRight)
                        {
                            initialHex = hex;
                        }
                    }

                    break;
                case InitialHexFrom.left:

                    int randomYLeft = Mathf.RoundToInt(Random.Range(area.GetStartPosition().y, area.GetEndPosition().y));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetHexCoordinates().x == area.GetStartPosition().x && hex.GetHexCoordinates().y == randomYLeft)
                        {
                            initialHex = hex;
                        }
                    }

                    break;
            }

            return initialHex;
        }

        static public float CalculateElevation(PatternType currentPattern, int horizontalSize, int verticalSize, int hexX, int hexY)
        {
            float rawElevetion = 0;
            float elevation = 0;

            float xPeak = 0;
            float yPeak = 0;

            float xElevetion = 0;
            float yElevetion = 0;

            switch (currentPattern)
            {
                case PatternType.island:


                    xPeak = horizontalSize / 2;
                    yPeak = verticalSize / 2;

                    xElevetion = (((hexX - xPeak) / 2) * ((hexX - xPeak) / 2) + xPeak);
                    yElevetion = (((hexY - yPeak) / 2) * ((hexY - yPeak) / 2) + yPeak);

                    rawElevetion = (10 * ((xPeak + yPeak) / 2)) / ((xElevetion + yElevetion) / 2);

                    elevation = Mathf.InverseLerp(0, 5, rawElevetion);

                    break;

                case PatternType.archipelago:

                    elevation = 0.31f;

                    break;

                case PatternType.northContinent:

                    yPeak = verticalSize;

                    int rnd = Random.Range(Mathf.RoundToInt(-yPeak/15), Mathf.RoundToInt(yPeak /15));

                    if (hexY+rnd > (yPeak - (yPeak/5)))
                    {
                        yElevetion = (((hexY - yPeak) / 2) * ((hexY - yPeak) / 2) + yPeak);
                        rawElevetion = (10*yPeak) / (yElevetion);
                        elevation = Mathf.InverseLerp(0, 5, rawElevetion) + 0.3f;
                    }
                    else if ((hexY+rnd > yPeak/10) && (hexY < (yPeak - (yPeak /5))))
                    {
                        yElevetion = ((hexY / 2) * (hexY / 2) + yPeak);
                        rawElevetion = (10 * yPeak) / yElevetion;
                        elevation = Mathf.InverseLerp(0, 5, rawElevetion);
                    }
                    else if (hexY+rnd <= yPeak/10)
                    {
                        elevation = -0.85f;
                    }


                    break;
            }

            return elevation;
        }

        public static float[,] GenerateNoiseMap(int horizontalSize, int verticalSize, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
            float[,] noiseMap = new float[horizontalSize, verticalSize];

            System.Random prng = new System.Random(seed);
            Vector2[] octaveOffsets = new Vector2[octaves];

            for (int octave = 0; octave < octaves; octave++)
            {
                float offsetX = prng.Next(-100000, 100000) + offset.x;
                float offsetY = prng.Next(-100000, 100000) + offset.y;
                octaveOffsets[octave] = new Vector2(offsetX, offsetY);
            }

            float maxNoiseHeight = float.MinValue;
            float minNoiseHeight = float.MaxValue;

            float halfWidth = horizontalSize / 2f;
            float halfHeight = verticalSize / 2f;


            for (int yCoordinate = 0; yCoordinate < verticalSize; yCoordinate++)
            {
                for (int xCoordinate = 0; xCoordinate < horizontalSize; xCoordinate++)
                {

                    float amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (int octave = 0; octave < octaves; octave++)
                    {
                        float xCurrent = (xCoordinate - halfWidth) / scale * frequency + octaveOffsets[octave].x;
                        float yCurrent = (yCoordinate - halfHeight) / scale * frequency + octaveOffsets[octave].y;

                        float perlinValue = Mathf.PerlinNoise(xCurrent, yCurrent) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= persistance;
                        frequency *= lacunarity;
                    }

                    if (noiseHeight > maxNoiseHeight)
                    {
                        maxNoiseHeight = noiseHeight;
                    }
                    else if (noiseHeight < minNoiseHeight)
                    {
                        minNoiseHeight = noiseHeight;
                    }
                    noiseMap[xCoordinate, yCoordinate] = noiseHeight;
                }
            }

            for (int yCoordinate = 0; yCoordinate < verticalSize; yCoordinate++)
            {
                for (int xCoordinate = 0; xCoordinate < horizontalSize; xCoordinate++)
                {
                    noiseMap[xCoordinate, yCoordinate] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[xCoordinate, yCoordinate]);
                }
            }

            return noiseMap;
        }

        public static int CalculateVegetationAmount(float hexElevation, float maxVegetationHexAttribute)
        {
            float rawAmount = 1 - Mathf.Abs(maxVegetationHexAttribute - hexElevation);

            int vegetationAmount = Mathf.RoundToInt(Mathf.InverseLerp(0.5f, 0.95f, rawAmount*rawAmount) * 10);

            return vegetationAmount;
        }
    }
}
