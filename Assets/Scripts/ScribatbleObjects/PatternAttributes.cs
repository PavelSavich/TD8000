using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    [CreateAssetMenu(fileName = "NewMapPatternAttributes", menuName = "MapGenerator/PatternAttributes", order = 1)]
    public class PatternAttributes : ScriptableObject
    {
        [SerializeField] private PatternType patternType = 0;

        [SerializeField] private MapRegion[] mapRegions;

        public PatternType GetPattern()
        {
            return patternType;
        }

        public MapRegion[] GetMapRegions()
        {
            return mapRegions;
        }
    }
}

