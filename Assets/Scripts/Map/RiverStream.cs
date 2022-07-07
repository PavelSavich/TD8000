using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TD.Map
{
    public class RiverStream : MonoBehaviour
    {
        [SerializeField] private bool first = false;
        [SerializeField] private bool last = false;

        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] List <Sprite> firstSprite = new List <Sprite>();
        [SerializeField] List<Sprite> middleSprite = new List<Sprite>();
        [SerializeField] List<Sprite> lastSprite = new List<Sprite>();

        public void SetAsFirst()
        {
            first = true;
        }

        public void SetAsLast()
        {
            last = true;
        }

        public void SetSprite()
        {
            if (first)
            {
                spriteRenderer.sprite = firstSprite[Random.Range(0,firstSprite.Count)];
            }
            else if (last)
            {
                spriteRenderer.sprite = lastSprite[Random.Range(0,lastSprite.Count)];   
            }
            else
            {
                spriteRenderer.sprite = middleSprite[Random.Range(0,middleSprite.Count)];
            }
        }    
    }
}

