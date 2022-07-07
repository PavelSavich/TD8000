using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    [CreateAssetMenu(fileName = "NewTreeAttributes", menuName = "MapGenerator/TreeAttributes", order = 3)]
    public class TreeAttributes : ScriptableObject
    {
        [SerializeField] private Sprite trunkSprite;
        [SerializeField] private Color32 trunkColor;
        [SerializeField] private Sprite leavesSprite;
        [SerializeField] private Color32 leavesColor;

        public Sprite GetTrunkSprite()
        {
            return trunkSprite;
        }

        public Color32 GetTrunkColor()
        {
            return trunkColor;
        }

        public Sprite GetLeavesSprite()
        {
            return leavesSprite;
        }

        public Color32 GetLeavesColor()
        {
            return leavesColor;
        }
    }
}


