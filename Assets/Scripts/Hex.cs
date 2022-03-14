using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD.Map
{
    public class Hex : MonoBehaviour
    {
        [Header("Hex Coordinates:")]
        [Tooltip("The relative X position of the current hex on the map.")]
        [SerializeField] private int xCoordinate = 0;

        [Tooltip("The relative Z position of the current hex on the map.")]
        [SerializeField] private int yCoordinate = 0;

        [Tooltip("The relative height of the current hex on the map.")]
        [SerializeField] private float hexElevation = 0;

        [SerializeField] private List<Hex> neighbourHexes = new List<Hex>();

        [SerializeField] private Text coordinatesText = null;

        [SerializeField] private HexAttributes hexAttributes = null;

        [SerializeField] private SpriteRenderer hexSpriteRenderer = null;

        public void SetX(int xToSet)
        {
            xCoordinate = xToSet;
        }

        public void SetY(int yToSet)
        {
            yCoordinate = yToSet;
        }

        public void SetElevation(float elevationToSet)
        {
            hexElevation = elevationToSet;
        }

        public void SetHexName(string hexName)
        {
            name =  $"Hex {hexName}";
            coordinatesText.text = hexName;
        }

        public int GetX()
        {
            return xCoordinate;
        }

        public int GetY()
        {
            return yCoordinate;
        }

        public float GetElevation()
        {
            return hexElevation;
        }

        public void SetNeighbourHexes(List<Hex> neighbours)
        {
            neighbourHexes = neighbours;
        }

        public List<Hex> GetNeighbourHexes()
        {
            return neighbourHexes;
        }

        public HexType GetHexType()
        {
            return hexAttributes.GetHexType();
        }

        public HexAttributes GetHexAttributes()
        {
            return hexAttributes;
        }

        public void SetHexAttributes(HexAttributes attributesToset)
        {
            hexAttributes = attributesToset;
        }

        public void SetHexSprite()
        {
            hexSpriteRenderer.sprite = hexAttributes.GetHexSprite();
        }
    }
}
