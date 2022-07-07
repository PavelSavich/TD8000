using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class HexTree : MonoBehaviour
    {
        [SerializeField] TreeAttributes treeAttributes = null;

        [SerializeField] private SpriteRenderer trunkSprite;
        [SerializeField] private SpriteRenderer leavesSprite;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private LayerMask treeLayer = new LayerMask();


        public LayerMask GetLayer()
        {
            return treeLayer;
        }

        public void SetTreeAttributes(TreeAttributes treeAttributes)
        {
            this.treeAttributes = treeAttributes;
        }

        public void SetSprites()
        {
            trunkSprite.sprite = treeAttributes.GetTrunkSprite();
            trunkSprite.color = treeAttributes.GetTrunkColor();
            leavesSprite.sprite = treeAttributes.GetLeavesSprite();
            leavesSprite.color = treeAttributes.GetLeavesColor();
            //TODO: Color and size randomization
        }
    }


}
