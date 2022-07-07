using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Cameras
{
    public class CameraController : MonoBehaviour
    {

        float movementSpeed;
        [SerializeField] float fastSpeed;
        [SerializeField] float normalSpeed;
        [SerializeField] float movementTime;

        [SerializeField] float screenBorderThickness;

        private bool screenLimitsSet = false;
        [SerializeField] private Vector3 maxScreenLimits = Vector3.zero;

        [SerializeField] Vector3 newPosition;

        void Start()
        {
            newPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            HandleKeyboardInput();
            HandleMouseInput();
            ApplyNewPosition();
        }

        void HandleKeyboardInput()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                movementSpeed = fastSpeed;
            }
            else
            {
                movementSpeed = normalSpeed;
            }

            newPosition += ((transform.up * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"))) * movementSpeed;
        }

        void HandleMouseInput()
        {
            Vector3 mouseMovement = Vector3.zero;

            Vector2 mousePosition = Input.mousePosition;

            if (mousePosition.y >= Screen.height - screenBorderThickness)
            {
                mouseMovement.z += 1;
            }
            else if (mousePosition.y <= screenBorderThickness)
            {
                mouseMovement.z -= 1;
            }
            if (mousePosition.x >= Screen.width - screenBorderThickness)
            {
                mouseMovement.x += 1;
            }
            else if (mousePosition.x <= screenBorderThickness)
            {
                mouseMovement.x -= 1;
            }

            newPosition += mouseMovement.normalized * movementSpeed;
        }

        private void ApplyNewPosition()
        {
            if (screenLimitsSet)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, 0, maxScreenLimits.x);
                newPosition.z = Mathf.Clamp(newPosition.z, 0, maxScreenLimits.z);
            }
            else
            {
                Debug.Log("Set Screen Limits to Camera Controller");
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        }


        public void FocusOnPosition(Vector3 transformToFocus)
        {
            newPosition = transformToFocus;
        }

        public void SetScreenLimits(Transform lastHexTransform)
        {
            screenLimitsSet = true;
            maxScreenLimits = lastHexTransform.position;
        }
    }
}
