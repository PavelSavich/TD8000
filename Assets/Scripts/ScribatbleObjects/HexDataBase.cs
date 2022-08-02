using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [CreateAssetMenu(fileName = "HexDatabase", menuName = "MapGenerator/HexDataBase", order = 3)]
    public class HexDataBase : ScriptableObject
    {
        [SerializeField] public List<HexAttributes> hexList = new List<HexAttributes>();

    }
}
