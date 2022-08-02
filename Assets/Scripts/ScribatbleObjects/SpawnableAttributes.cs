using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    [CreateAssetMenu(fileName = "SpawnableAttributes", menuName = "MapGenerator/SpawnableAttributes", order = 4)]
    public class SpawnableAttributes : ScriptableObject
    {
        [SerializeField] private Sprite primarySprite;
        [SerializeField] private Color32 primaryColor;
        [SerializeField] private Sprite secondarySprite;
        [SerializeField] private Color32 secondaryColor;

        public Sprite GetPrimarySprite()
        {
            return primarySprite;
        }

        public Color32 GetPrimaryColor()
        {
            return primaryColor;
        }

        public Sprite GetSecondarySprite()
        {
            return secondarySprite;
        }

        public Color32 GetSecondaryColor()
        {
            return secondaryColor;
        }
    }
}


