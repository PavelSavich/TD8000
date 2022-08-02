using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.EventSystems;

namespace TD.Cameras
{
    public class Minimap : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private RectTransform minimapRect = null;
        [SerializeField] private Vector3 mapSize = new Vector3();
        [SerializeField] private float offset = 0;

        private CameraController cameraController;

        private void Update()
        {
            if(cameraController != null) { return; }

            cameraController = FindObjectOfType<CameraController>() ;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            MoveCamera();
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveCamera();
        }

        public void SetMapSize(Vector3 lastHexPos)
        {
            mapSize = lastHexPos;
        }

        private void MoveCamera()
        {
            Vector2 mousePose = Input.mousePosition;

            if(!RectTransformUtility.ScreenPointToLocalPointInRectangle (minimapRect, mousePose,null, 
                                                                         out Vector2 localPoint )) { return; }

            Vector2 lerp = new Vector2((localPoint.x - minimapRect.rect.x) / minimapRect.rect.width,
                                       (localPoint.y - minimapRect.rect.y) / minimapRect.rect.height);

            Vector3 newCameraViewPortPos = new Vector3(Mathf.Lerp(0, mapSize.x, lerp.x),
                                               0,
                                               Mathf.Lerp(0 , mapSize.z, lerp.y));

            cameraController.FocusOnPosition(newCameraViewPortPos + new Vector3(0, 0, offset));
        }
    }
}


