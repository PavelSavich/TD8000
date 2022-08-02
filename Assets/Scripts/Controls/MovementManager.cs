using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Controls
{
    public class MovementManager : MonoBehaviour
    {
        [SerializeField] private ActiveArmy selectedArmy = null;
        [SerializeField] private ActiveArmy currentArmy = null;

        [SerializeField] Hex startingHex = null;
        [SerializeField] private Vector2Int startingHexCoordinates = new Vector2Int();


        [SerializeField] Hex pointingOnHex = null;
        [SerializeField] private Vector2Int pointingOnHexCoordinates = new Vector2Int();

        [SerializeField] Hex destinitionHex = null;
        [SerializeField] private Vector2Int destinitionHexCoordinates = new Vector2Int();

        private bool searchingForPath = true;

        private Hex searchCenter;
        private List<Hex> potentialPath = new List<Hex>();

        void Start()
        {

        }

        void Update()
        {
            if(selectedArmy == null || startingHex == null || pointingOnHex == null) // DEBUG
            {
                ClearPath();
                return;
            }

            FindPath(startingHex);
            ShowPath();
            ResetPath(selectedArmy);
        }

        public void FindPath(Hex hex)
        {
            if (searchingForPath)
            {
                searchCenter = hex;

                potentialPath.Add(searchCenter);

                if (searchCenter == pointingOnHex)
                {
                    searchingForPath = false;
                    return;
                }

                foreach (Hex neighbour in searchCenter.GetNeighbourHexes())
                {
                    if (Vector3.Distance(searchCenter.transform.position, pointingOnHex.transform.position) > 
                        Vector3.Distance(neighbour.transform.position, pointingOnHex.transform.position))
                    {
                        searchCenter = neighbour;
                    }
                }

                FindPath(searchCenter);
            }
        }

        private void ShowPath()
        {
            if (potentialPath.Count > 0)
            {
                foreach (Hex hexInPath in potentialPath)
                {
                    hexInPath.GetComponentInChildren<MouseResponder>().HighlightHex();
                }
            }
        }

        private void HidePath()
        {
            if (potentialPath.Count > 0)
            {
                foreach (Hex hexInPath in potentialPath)
                {
                    hexInPath.GetComponentInChildren<MouseResponder>().DeHighlightHex();
                }
            }
        }

        public void ResetPath(ActiveArmy selectedArmy)
        {

            if(currentArmy != selectedArmy)
            {
                currentArmy = selectedArmy;
                ClearPath();
            }

            if(startingHex == null || pointingOnHex == null)
            {
                ClearPath();
            }
        }

        public void ClearPath()
        {
            HidePath();
            potentialPath.Clear();
            searchingForPath = true;
        }

        public void MoveArmy()
        {

        }

        public void SetActiveArmy(ActiveArmy currentActiveArmy)
        {
            ClearPath();
            selectedArmy = currentActiveArmy;
        }

        public void SetStatringHex(Hex hex)
        {
            ClearPath();
            startingHex = hex;
            startingHexCoordinates = hex.GetHexCoordinates();
        }

        public void SetPointingOnHex(Hex hex)
        {
            ClearPath();
            pointingOnHex = hex;
            pointingOnHexCoordinates = hex.GetHexCoordinates();
        }

    }
}
