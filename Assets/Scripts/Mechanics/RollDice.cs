using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Mechanics
{
    public class RollDice : MonoBehaviour
    {
        public static bool D10 (int successAt)
        {
            int rollResult = Random.Range(1, 11);
            return rollResult <= successAt;
        }
    }
}
