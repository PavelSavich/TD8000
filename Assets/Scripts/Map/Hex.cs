using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TD.Map
{
    public class Hex : MonoBehaviour
    {
        [Header("Hex Coordinates:")]
        [SerializeField] private Vector2Int hexCoordinate = new Vector2Int();

        [Header("Elevation:")]
        [Range(0,1)][SerializeField] private float hexPatternElevation = 0;
        [Range(0,1)][SerializeField] private float hexNoiseElevation = 0;
        [Range(0, 1)][SerializeField] private float hexElevation = 0;

        [Header("Rivers:")]
        [SerializeField] private bool hasRiverStream = false;
        [SerializeField] private RiverStream riverStream = null;

        [Header("Trees:")]
        [SerializeField] private int vegetationAmount = 0;

        [SerializeField] private List<Hex> neighbourHexes = new List<Hex>();

        [SerializeField] private HexAttributes hexAttributes = null;

        [SerializeField] private SpriteRenderer hexSpriteRenderer = null;
        [SerializeField] private SpriteRenderer minimapSpriteRenderer = null;

        [SerializeField] private HexAreaSpawner spawner = null;

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
            if (hexAttributes != null)
            {
                hexSpriteRenderer.sprite = hexAttributes.GetHexSprite();
                minimapSpriteRenderer.sprite = hexAttributes.GetHexSprite();
            }
        }

        public void SetHexColor()
        {
            if (hexAttributes != null)
            {
                hexSpriteRenderer.color *= hexElevation;
            }
        }

        public bool GetHasRiverStream()
        {
            return hasRiverStream;
        }

        public void SetHasRiverStream(bool haveStream)
        {
            hasRiverStream = haveStream;
        }

        public RiverStream GetRiverStream()
        {
            return riverStream;
        }

        public void SetRiverStream(RiverStream streamToSet)
        {
            riverStream = streamToSet;
        }

        public void SetVegetationAmount(int amountToSet)
        {
            vegetationAmount = amountToSet;
        }

        public int GetVegetationAmount()
        {
            return vegetationAmount;
        }

        public HexAreaSpawner GetSpawner()
        {
            return spawner;
        }

    }
}
