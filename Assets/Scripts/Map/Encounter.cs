using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class Encounter : MonoBehaviour
    {
        [SerializeField] private EncounterAttributes encounterAttributes = null;

        [SerializeField] string encounterName = "";

        [SerializeField] int currentLevel = 0;
        [SerializeField] int currentAttack = 0;
        [SerializeField] int currentDefence = 0;
        [SerializeField] int currentlHP = 0;

        public void SetEncounterAttributes(EncounterAttributes attributesToSet)
        {
            encounterAttributes = attributesToSet;
        }

        public void SetEncounterStats()
        {
            encounterName = encounterAttributes.GetEncounterName();

            if (encounterAttributes.GetEncounterType() == EncounterType.Unit)
            {
                currentLevel = encounterAttributes.GetInitialLevel();
                currentAttack = encounterAttributes.GetInitialAttack();
                currentDefence = encounterAttributes.GetInitialDefence();
                currentlHP = encounterAttributes.GetInitialHP();
            }
        }

        public EncounterAttributes GetEncounterAttributes()
        {
            return encounterAttributes;
        }
    }
}


