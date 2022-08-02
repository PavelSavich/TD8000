using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Cameras
{
    public class CamerasDirector : MonoBehaviour
    {
        private void Start()
        {
            SetupCameras();
        }

        public void SetupCameras()
        {
            MapParent mapParent = FindObjectOfType<MapParent>();

            CameraController cameraController = FindObjectOfType<CameraController>();
            MinimapCamera minimapCamera = FindObjectOfType<MinimapCamera>();
            Minimap minimap = FindObjectOfType<Minimap>();

            if (mapParent != null)
            {
                Debug.Log("map parent found");
                Vector2Int mapSize = mapParent.GetMapSize();
                Vector2Int lastHexCoordinates = new Vector2Int(mapSize.x - 1, mapSize.y - 1);

                if (cameraController != null)
                {
                    cameraController.SetScreenLimits(mapParent.GetLastHexPos());
                }

                if (minimapCamera != null)
                {
                    minimapCamera.SetPosition(mapParent.GetLastHexPos());
                }

                if (minimap != null)
                {
                    minimap.SetMapSize(mapParent.GetLastHexPos());
                }
            }
        }


    }
}


