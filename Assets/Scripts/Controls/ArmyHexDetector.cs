using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Controls
{
    public class ArmyHexDetector : MonoBehaviour
    {
        [SerializeField] private ActiveArmy parentArmy = null;

        private void OnTriggerEnter(Collider other)
        {
            Hex steppedOnHex = other.GetComponentInParent<Hex>();

            if (steppedOnHex != null)
            {
                parentArmy.SetOcupiedHex(steppedOnHex);
            }
        }
    }
}


