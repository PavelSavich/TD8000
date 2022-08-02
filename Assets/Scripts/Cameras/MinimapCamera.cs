using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Cameras
{
    public class MinimapCamera : MonoBehaviour
    {
        [SerializeField] Camera minimapCamera = null;

        public void SetPosition (Vector3 lastHexPos)
        {
            transform.position = new Vector3(lastHexPos.x / 2, transform.position.y, lastHexPos.z / 2);
            minimapCamera.orthographicSize = Mathf.Max(lastHexPos.x / 2, lastHexPos.z / 2) + 2;
        }
    }
}
