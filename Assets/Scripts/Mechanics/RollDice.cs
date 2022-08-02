using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Mechanics
{
    public static class RollDice
    {
        public static int Roll(int diceDalue)
        {
            int rollResult = Random.Range(1, diceDalue + 1);
            return rollResult;
        }

        public static List<int> Roll( int diceDalue, int numberOfDice)
        {
            List<int> results = new List<int>();

            for (int currentRoll = 0; currentRoll < numberOfDice; currentRoll++)
            {
                int rollResult = Random.Range(1, diceDalue + 1);

                results.Add(rollResult);
            }

            return results;
        }
    }
}
