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
                        if (hex.GetX() == randomXCenter && hex.GetY() == randomYCenter)
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
                        if (hex.GetX() == randomXTop && hex.GetY() == area.GetEndPosition().y)
                        {
                            initialHex = hex;
                        }
                    }

                    break;

                case InitialHexFrom.bottom:

                    int randomXBottom = Mathf.RoundToInt(Random.Range(area.GetStartPosition().x, area.GetEndPosition().x));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetX() == randomXBottom && hex.GetY() == area.GetStartPosition().y)
                        {
                            initialHex = hex;
                        }
                    }

                    break;

                case InitialHexFrom.right:

                    int randomYRight = Mathf.RoundToInt(Random.Range(area.GetStartPosition().y, area.GetEndPosition().y));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetX() == area.GetEndPosition().x && hex.GetY() == randomYRight)
                        {
                            initialHex = hex;
                        }
                    }

                    break;
                case InitialHexFrom.left:

                    int randomYLeft = Mathf.RoundToInt(Random.Range(area.GetStartPosition().y, area.GetEndPosition().y));

                    foreach (Hex hex in area.GetHexList())
                    {
                        if (hex.GetX() == area.GetStartPosition().x && hex.GetY() == randomYLeft)
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
            float elevation = 0;

            float xPeak = 0;
            float yPeak = 0;

            float xElevetion = 0;
            float yElevetion = 0;

            switch (currentPattern)
            {
                case PatternType.none:
                    break;

                case PatternType.island:


                    xPeak = horizontalSize / 2;
                    yPeak = verticalSize / 2;

                    xElevetion = (((hexX - xPeak) / 2) * ((hexX - xPeak) / 2) + xPeak);
                    yElevetion = (((hexY - yPeak) / 2) * ((hexY - yPeak) / 2) + yPeak);

                    elevation = (10 * ((xPeak + yPeak) / 2)) / ((xElevetion + yElevetion) / 2);

                    break;

                case PatternType.north:
                    break;
                case PatternType.center:
                    break;
                case PatternType.edge:
                    break;

                case PatternType.northContinent:

                    yPeak = verticalSize;

                    if (hexY > (yPeak - (yPeak/5)))
                    {
                        yElevetion = (((hexY - yPeak) / 2) * ((hexY - yPeak) / 2) + yPeak);
                        elevation = (10*yPeak) / (yElevetion);
                    }
                    if ((hexY > yPeak/10) && (hexY < (yPeak - (yPeak /5))))
                    {
                        yElevetion = ((hexY / 2) * (hexY / 2) + yPeak);
                        elevation = (10 * yPeak) / yElevetion;
                    }
                    if (hexY <= yPeak/10)
                    {
                        elevation = -1;
                    }


                    break;
            }

            return elevation;
        }
    }
}
