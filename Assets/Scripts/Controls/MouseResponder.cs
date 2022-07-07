using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Controls
{
    public class MouseResponder : MonoBehaviour
    {
        [SerializeField] SpriteRenderer sprite = null;
        [SerializeField] Color idleColor = new Color();
        [SerializeField] Color pressedColor = new Color();

        private void Start()
        {
            idleColor = sprite.color;
        }

        private void OnMouseEnter()
        {
            sprite.enabled = true;
            sprite.color = idleColor;
        }

        private void OnMouseExit()
        {
            sprite.enabled=false;
            sprite.color = idleColor;
        }

        private void OnMouseDown()
        {
            sprite.color = pressedColor;
        }

        private void OnMouseUp()
        {
            sprite.color = idleColor;
        }
    }
}
