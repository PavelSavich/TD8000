using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    [CreateAssetMenu(fileName = "EncounterAttributes", menuName = "MapGenerator/EncounterAttributes", order = 6)]
    public class EncounterAttributes : ScriptableObject
    {
        [SerializeField] SpawnableAttributes spawnableAttributes = null;

        [SerializeField] EncounterType encounterType;
        [SerializeField] string encounterName;

        [SerializeField][TextArea(5, 15)] string encounterDescription;
        [SerializeField][TextArea(5, 15)] string encounterResolution;

        [SerializeField] int bonusPoints = 0;

        [SerializeField] int initialLevel = 0;
        [SerializeField] int initialAttack = 0;
        [SerializeField] int initialDefence = 0;
        [SerializeField] int initialHP = 0;

        public SpawnableAttributes GetSpawnablAttributes()
        {
            return spawnableAttributes;
        }

        public EncounterType GetEncounterType()
        {
            return encounterType;
        }

        public string GetEncounterName()
        {
            return encounterName;
        }

        public string GetEncounterDescription()
        {
            return encounterDescription;
        }

        public string GetEncounterResolution()
        {
            return encounterResolution;
        }

        public int GetBonusPoints()
        {
            return bonusPoints;
        }

        public int GetInitialLevel()
        {
            return initialLevel;
        }

        public int GetInitialAttack()
        {
            return initialAttack;
        }

        public int GetInitialDefence()
        {
            return initialDefence;
        }

        public int GetInitialHP()
        {
            return initialHP;
        }

    }
}
