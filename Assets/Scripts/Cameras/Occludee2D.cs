using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Cameras
{
    public class Occludee2D : MonoBehaviour
    {
        [SerializeField]  List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

        void Start()
        {
            ActivateSprites(false);
        }

        private void OnTriggerStay(Collider collision)
        {
            if (collision.transform.tag == "CameraViewport")
            {
                ActivateSprites(true);
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.transform.tag == "CameraViewport")
            {
                ActivateSprites(false);
            }
        }

        private void ActivateSprites(bool activate)
        {
            if (spriteRenderers.Count==0)
            {
                return;
            }

            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = activate;
            }
        }



    }
}
