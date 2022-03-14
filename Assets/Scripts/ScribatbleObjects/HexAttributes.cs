using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [CreateAssetMenu(fileName = "NewHexTypeAttributes", menuName = "MapGenerator/HexAttributes", order = 2)]
    public class HexAttributes : ScriptableObject
    {
        [SerializeField] private HexType hexType = 0;
        [SerializeField] private Sprite hexSprite; //TODO: 1. Make it an array;

        [Header("Elevation")]
        [Range(-1,10)][SerializeField] private float minTypeElevation = 0f;
        [Range(-1, 10)] [SerializeField] private float maxTypeElevation = 0f;

        public HexType GetHexType()
        {
            return hexType;
        }

        public Sprite GetHexSprite() //TODO: 2. Get Random Sprite from array...
        {
            return hexSprite;
        }

        public float GetMinTypeElevation()
        {
            return minTypeElevation;
        }

        public float GetMaxTypeElevation()
        {
            return maxTypeElevation;
        }
    }
}
