using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [System.Serializable]
    public class MapRegion
    {
        [SerializeField] private CardinalAreas regionCardinalPosition = 0;

        public CardinalAreas GetRegionCardinal()
        {
            return regionCardinalPosition;
        }

        [SerializeField] InitialHexFrom getInitialHexFrom = 0;

        public InitialHexFrom GetInitialHexFrom()
        {
            return getInitialHexFrom;
        }

        [SerializeField] private List<HexesInRegion> hexesInRegion;

        public List<HexesInRegion> GetHexesInRegion()
        {
            return hexesInRegion;
        }
    }
}


