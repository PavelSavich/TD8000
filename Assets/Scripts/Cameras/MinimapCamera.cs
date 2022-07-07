using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Cameras
{
    public class MinimapCamera : MonoBehaviour
    {
        [SerializeField] Camera minimapCamera = null;

        public void SetPosition (Transform lastHexTransfrom)
        {
            transform.position = new Vector3(lastHexTransfrom.position.x / 2, transform.position.y, lastHexTransfrom.position.z / 2);
            minimapCamera.orthographicSize = Mathf.Max(lastHexTransfrom.position.x / 2, lastHexTransfrom.position.z / 2) + 2;
        }
    }
}
