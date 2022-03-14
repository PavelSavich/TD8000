using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [System.Serializable]
    public class AreaInMap
    {
        [SerializeField] private CardinalAreas cardinalArea = 0;

        public CardinalAreas GetCardinalArea()
        {
            return cardinalArea;
        }

        [SerializeField] private Vector2 startPostion = new Vector2(0, 0);

        public Vector2 GetStartPosition()
        {
            return startPostion;
        }

        public void SetStartPosition(Vector2 position)
        {
            startPostion = position;
        }

        [SerializeField] private Vector2 endPostion = new Vector2(0, 0);

        public Vector2 GetEndPosition()
        {
            return endPostion;
        }

        public void SetEndPosition(Vector2 position)
        {
            endPostion = position;
        }

        [SerializeField] private List<Hex> hexesInArea = new List<Hex>();

        public List<Hex> GetHexList()
        {
            return hexesInArea;
        }

        public void AddToHexList(Hex hex)
        {
            hexesInArea.Add(hex);
        }
    }
}
