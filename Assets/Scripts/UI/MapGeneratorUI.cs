using System.Collections;
using System.Collections.Generic;
using TD.Cameras;
using TD.Map;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


namespace TD.UI
{
    public class MapGeneratorUI : MonoBehaviour
    {
        [SerializeField] MapGenerator mapGenerator = null;
        [SerializeField] TMP_InputField seedInput = null;
        [SerializeField] TMP_Text seedPlaceHolderText = null;
        [SerializeField] TMP_Text seedInputText = null;

        public void ClearMap()
        {
            MapParent map = FindObjectOfType<MapParent>();

            if (map != null)
            {
                Destroy(map);
            }
        }

        public void SetMapSizeDd(int dD)
        {
            int sizeToSet = 50;

            switch (dD)
            {
                case 0: //Small
                    sizeToSet = 50;
                    break;

                case 1: //Medium
                    sizeToSet = 75;
                    break;

                case 2: //Large
                    sizeToSet = 100;
                    break;

                case 3: //Huge
                    sizeToSet = 200;
                    break;

                case 4: //Gigantic
                    sizeToSet = 300;
                    break;
            }

            mapGenerator.SetSize(sizeToSet);
        }

        public void SetMapType(int typeToSet)
        {
            mapGenerator.SetPattern(typeToSet);
        }

        public void ToggleRandomSeed (bool isRandom)
        {
            seedInput.interactable = !isRandom;
            mapGenerator.SetRrandomSeed(isRandom);
        }

        public void GenerateMap()
        {
            MapParent map = FindObjectOfType<MapParent>();

            if (map != null)
            {
                Destroy(map.gameObject);
            }

            if (seedInput.interactable)
            {
                string input = seedInputText.text;

                int.TryParse(input, out int seedValue);
                mapGenerator.SetSeed(seedValue);
            }

            mapGenerator.GenerateMap();

            if (!seedInput.interactable)
            {
                seedPlaceHolderText.text = mapGenerator.GetSeed().ToString();
            }

            CamerasDirector camerasDirector = FindObjectOfType<CamerasDirector>();
            camerasDirector.SetupCameras();
        }
    }
}
