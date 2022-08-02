using System.Collections;
using System.Collections.Generic;
using TD.Map;
using UnityEngine;

namespace TD.Controls
{
    public class ActiveArmy : MonoBehaviour
    {
        [SerializeField] private Vector3 startingPosition = new Vector3();
        [SerializeField] private Hex ocupiedHex = null; 
        [SerializeField] private Vector2Int hexCoordinates = new Vector2Int();


        [SerializeField] private bool isActiveArmy = false;

        [SerializeField] private int movmentPoints;
        [SerializeField] private int movementLeft;

        MovementManager movementManager;

        private void Awake()
        {

        }

        void Start()
        {
            SetActiveArmy(); //TODO: Remove it from here;
            transform.position = startingPosition;
        }

        void Update()
        {

        }



        public void SetOcupiedHex(Hex hex)
        {
            ocupiedHex = hex;
            hexCoordinates = hex.GetHexCoordinates();

            if (movementManager == null)
            {
                movementManager = FindObjectOfType<MovementManager>();
            }

            movementManager.SetStatringHex(ocupiedHex);

        }

        public void SetActiveArmy()
        {
            isActiveArmy=true;

            if (movementManager == null)
            {
                movementManager = FindObjectOfType<MovementManager>();
            }

            movementManager.SetActiveArmy(this);
        }

        public Hex GetOcupiedHex()
        {
            return ocupiedHex;
        }
    }
}
