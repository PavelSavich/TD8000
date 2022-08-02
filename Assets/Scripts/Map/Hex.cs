using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TD.Saving;

namespace TD.Map
{
    public class Hex : MonoBehaviour//, ISaveable
    {
        [Header("Hex Coordinates:")]
        [SerializeField] private Vector2Int hexCoordinates = new Vector2Int();

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
            hexCoordinates = coordinatesToSet;
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

        public Vector2Int GetHexCoordinates()
        {
            return hexCoordinates;
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

        //[System.Serializable]
        //private struct SaveInfo
        //{
        //    public GameObject parent;
        //    public Transform transform;
        //    public Vector2Int hexCoordinates;
        //    public float hexElevation;
        //    public bool hasRiverStream;
        //    public int vegetationAmount;
        //    public List<Hex> neighbourHexes;
        //    public HexAttributes hexAttributes;


        //    public SaveInfo(GameObject parent,
        //                    Transform transform,
        //                    Vector2Int hexCoordinates,
        //                    float hexElevation,
        //                    bool hasRiverStream,
        //                    int vegetationAmount,
        //                    List<Hex> neighbourHexes,
        //                    HexAttributes hexAttributes)
        //    {
        //        this.parent = parent;
        //        this.transform = transform;
        //        this.hexCoordinates = hexCoordinates;
        //        this.hexElevation = hexElevation;
        //        this.hasRiverStream = hasRiverStream;
        //        this.vegetationAmount = vegetationAmount;
        //        this.neighbourHexes = neighbourHexes;
        //        this.hexAttributes = hexAttributes;
        //    }
        //}

        //public object CaptureState()
        //{
        //    SaveInfo saveInfo = new SaveInfo(FindObjectOfType<MapParent>().gameObject,
        //                                     transform,
        //                                     hexCoordinates,
        //                                     hexElevation, 
        //                                     hasRiverStream, 
        //                                     vegetationAmount, 
        //                                     neighbourHexes, 
        //                                     hexAttributes);

        //    return saveInfo;
        //}

        //public void RestoreState(object state)
        //{
        //    SaveInfo savedInfo = (SaveInfo)state;
        //    transform.parent = savedInfo.parent.transform;
        //    transform.transform.position = savedInfo.transform.position;
        //    transform.rotation = savedInfo.transform.rotation;
        //    hexCoordinates = savedInfo.hexCoordinates;
        //    hexElevation = savedInfo.hexElevation;
        //    hasRiverStream = savedInfo.hasRiverStream;
        //    vegetationAmount = savedInfo.vegetationAmount;
        //    neighbourHexes = savedInfo.neighbourHexes;
        //    hexAttributes = savedInfo.hexAttributes;
        //}
    }
}
