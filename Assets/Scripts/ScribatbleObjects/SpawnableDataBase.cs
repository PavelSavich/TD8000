using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Map
{
    [CreateAssetMenu(fileName = "SpawnableDatabase", menuName = "MapGenerator/SpawnableDataBase", order = 5)]
    public class SpawnableDataBase : ScriptableObject
    {
        [SerializeField] public List<SpawnableAttributes> spawnableList = new List<SpawnableAttributes>();
    }
}
