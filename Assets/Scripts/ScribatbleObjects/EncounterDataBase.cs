using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [CreateAssetMenu(fileName = "EncounterDataBase", menuName = "MapGenerator/EncounterDataBase", order = 7)]

    public class EncounterDataBase : ScriptableObject
    {
        [SerializeField] public List<EncounterAttributes> encounterList = new List<EncounterAttributes>();
     }
}


