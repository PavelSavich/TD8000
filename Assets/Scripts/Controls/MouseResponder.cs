using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Controls
{
    public class MouseResponder : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite = null;
        [SerializeField] Color idleColor = new Color();
        [SerializeField] Color pressedColor = new Color();

        MovementManager movementManager;

        private void Start()
        {
            idleColor = sprite.color;
        }

        private void OnMouseEnter()
        {
            Hex pointingOnHex = gameObject.GetComponentInParent<Hex>();

            if (pointingOnHex != null)
            {
                if (movementManager == null)
                {
                    movementManager = FindObjectOfType<MovementManager>();
                }
                else
                {
                    movementManager.SetPointingOnHex(pointingOnHex);
                }
            }

            HighlightHex();
        }

        private void OnMouseExit()
        {
            DeHighlightHex();
        }

        private void OnMouseDown()
        {
            sprite.color = pressedColor;
        }

        private void OnMouseUp()
        {
            sprite.color = idleColor;
        }

        public void HighlightHex()
        {
            sprite.enabled = true;
            sprite.color = idleColor;
        }

        public void DeHighlightHex()
        {
            sprite.enabled = false;
            sprite.color = idleColor;
        }
    }
}
