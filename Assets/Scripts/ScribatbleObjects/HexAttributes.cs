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
        [Range(0,1)][SerializeField] private float typeElevation = 0f;

        [Header("Vegetation")]
        [SerializeField] List<SpawnableAttributes> treesGrowOnHexType = new List<SpawnableAttributes>();

        public HexType GetHexType()
        {
            return hexType;
        }

        public Sprite GetHexSprite() //TODO: 2. Get Random Sprite from array...
        {
            return hexSprite;
        }

        public float GetTypeElevation()
        {
            return typeElevation;
        }

        public SpawnableAttributes GetRandomTreeType()
        {
            return treesGrowOnHexType[Random.Range(0,treesGrowOnHexType.Count)];
        }
    }
}
