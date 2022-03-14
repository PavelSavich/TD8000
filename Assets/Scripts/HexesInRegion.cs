using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [System.Serializable]
    public class HexesInRegion
    {
        [SerializeField] public HexAttributes hexAttributes;

        [SerializeField] [Range(0, 10)] public int amountInRegion = 0;

        //TODO: Not suppoused to be here..
        [SerializeField] [Range(0, 10)] public int normalizationFactor = 0;

        public HexAttributes GetHexAttributes()
        {
            return hexAttributes;
        }

        public int GetAmountInRegion()
        {
            return amountInRegion;
        }

        //TODO: Not suppoused to be here...
        public int GetNormalizationFactor()
        {
            return normalizationFactor;
        }
    }
}


