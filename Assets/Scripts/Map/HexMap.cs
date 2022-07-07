using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    using int2 = Vector2Int;
    using float2 = Vector2;
    public enum Direction
    {
        NE,
        E,
        SE,
        SW,
        W,
        NW
    }

    [System.Serializable]
    public class HexMap
    {
        public HexMap(int2 size)
        {
             hexMap = new Hex[size.x,size.y];
        }

        private Hex[,] hexMap;

        public Hex[,] GetHexMap()
        {
            return hexMap;
        }

        public void SetHexToMap(Hex hex)
        {
            hexMap[hex.GetHexCoordinate().x,hex.GetHexCoordinate().y] = hex;
        }

        public Hex GetHexFromMap(int2 coord)
        {
            if(hexMap[coord.x, coord.y] != null)
            {
                return hexMap[coord.x, coord.y];

            }

            return null;
        }

        public Hex GetNeighbour(int2 coord, Direction direction, int2 mapSize)
        {
            int2 neighbour = new int2(0,0);

            if (coord.y % 2 == 0)
            {
                switch (direction)
                {
                    case Direction.NE:

                        neighbour.x = coord.x + 1;
                        neighbour.y = coord.y + 1;

                        break;

                    case Direction.E:

                        neighbour.x = coord.x + 1;
                        neighbour.y = coord.y;

                        break;

                    case Direction.SE:

                        neighbour.x = coord.x + 1;
                        neighbour.y = coord.y - 1;

                        break;

                    case Direction.SW:

                        neighbour.x = coord.x;
                        neighbour.y = coord.y - 1;

                        break;

                    case Direction.W:

                        neighbour.x = coord.x - 1;
                        neighbour.y = coord.y;

                        break;

                    case Direction.NW:

                        neighbour.x = coord.x;
                        neighbour.y = coord.y + 1;

                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.NE:

                        neighbour.x = coord.x;
                        neighbour.y = coord.y + 1;

                        break;

                    case Direction.E:

                        neighbour.x = coord.x + 1;
                        neighbour.y = coord.y;

                        break;

                    case Direction.SE:

                        neighbour.x = coord.x;
                        neighbour.y = coord.y - 1;

                        break;

                    case Direction.SW:

                        neighbour.x = coord.x - 1;
                        neighbour.y = coord.y - 1;

                        break;

                    case Direction.W:

                        neighbour.x = coord.x - 1;
                        neighbour.y = coord.y;

                        break;

                    case Direction.NW:

                        neighbour.x = coord.x - 1;
                        neighbour.y = coord.y + 1;

                        break;
                }
            }

            if (neighbour.x >= 0 && neighbour.x < mapSize.x && neighbour.y >= 0 && neighbour.y < mapSize.y)
            {
                return GetHexFromMap(neighbour);

            }
            else
            {
                return null;
            }

        }
    }
}

