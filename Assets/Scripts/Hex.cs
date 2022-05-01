using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD.Map
{
    public class Hex : MonoBehaviour
    {
        [Header("Hex Coordinates:")]
        [Tooltip("The relative X and Z positions of the current hex on the hex map.")]
        [SerializeField] private Vector2Int hexCoordinate = new Vector2Int();

        [Tooltip("The relative height of the current hex on the map.")]
        [Range(0,1)][SerializeField] private float hexPatternElevation = 0;
        [Range(0,1)][SerializeField] private float hexNoiseElevation = 0;
        [Range(0, 1)][SerializeField] private float hexElevation = 0;

        [SerializeField] private bool hasRiverStream = false;
        [SerializeField] private RiverStream riverStream = null;

        [SerializeField] private List<Hex> neighbourHexes = new List<Hex>();

        [SerializeField] private HexAttributes hexAttributes = null;

        [SerializeField] private SpriteRenderer hexSpriteRenderer = null;

        public void SetHexCoordinates(Vector2Int coordinatesToSet)
        {
            hexCoordinate = coordinatesToSet;
        }


        public void SetHexPatternElevation(float elevationToSet)
        {
            hexPatternElevation = elevationToSet;
        }

        public void SetHexNoiseElevation(float elevationToSet)
        {
            hexNoiseElevation = elevationToSet;
        }

        public void SetElevation()
        {
            hexElevation = (hexPatternElevation + hexNoiseElevation)/2;
        }

        public void SetHexName(string hexName)
        {
            name =  $"Hex {hexName}";
        }

        public Vector2Int GetHexCoordinate()
        {
            return hexCoordinate;
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

        public bool GetHasRiverStream()
        {
            return hasRiverStream;
        }

        public void SetRiverStream(bool haveStream)
        {
            hasRiverStream = haveStream;
        }
    }
}
