using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class HexSpawnable : MonoBehaviour
    {
        [SerializeField] SpawnableAttributes spawnableAttributes = null;

        [SerializeField] private SpriteRenderer primarySprite;
        [SerializeField] private SpriteRenderer secondarySprite;
        [SerializeField] private Transform baseTransform;
        [SerializeField] private Vector3 spreadOnHex = new Vector3();
        [SerializeField] private float spawnBoxOverlap;
        [SerializeField] private LayerMask spawnableLayer = new LayerMask();


        public LayerMask GetLayer()
        {
            return spawnableLayer;
        }

        public Vector3 GetSpread()
        {
            return spreadOnHex;
        }

        public float GetBoxOverlap()
        {
            return spawnBoxOverlap;
        }

        public SpriteRenderer GetPrimarySprite()
        {
            return primarySprite;
        }

        public SpriteRenderer GetSecondarySprite()
        {
            return secondarySprite;
        }

        public void SetSpawnableAttributes(SpawnableAttributes attributesToSet)
        {
            this.spawnableAttributes = attributesToSet;
        }

        public void SetSprites()
        {
            float variation = Random.Range(1f, 2f);

            primarySprite.sprite = spawnableAttributes.GetPrimarySprite();
            primarySprite.color = spawnableAttributes.GetPrimaryColor() * new Color(variation, variation, variation, variation);

            secondarySprite.sprite = spawnableAttributes.GetSecondarySprite();
            secondarySprite.color = spawnableAttributes.GetSecondaryColor() * new Color(variation, variation, variation, variation);

            transform.localScale *= variation;
        }
    }


}
