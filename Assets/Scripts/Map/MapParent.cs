using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Saving;


namespace TD.Map
{
    public class MapParent : MonoBehaviour, ISaveable
    {
        [SerializeField] private string mapName = "";
        [SerializeField] public HexMap hexMap = null;
        [SerializeField] private Vector2Int mapSize = new Vector2Int(12, 12);
        [SerializeField] private Vector3 lastHexPos = new Vector3();

        public void SetMapSize(Vector2Int sizeToSet)
        {
            mapSize = sizeToSet;
        }

        public Vector2Int GetMapSize()
        {
            return mapSize;
        }

        public void SetLastHexPos(Vector3 positionToSet)
        {
            lastHexPos = positionToSet;
        }

        public Vector3 GetLastHexPos()
        {
            return lastHexPos;
        }

        [System.Serializable]
        private struct SaveInfo
        {
            public string mapName;
            public int mapSizeX;
            public int mapSizeY;

            public SaveInfo(string mapName, int mapSizeX, int mapSizeY)
            {
                this.mapName = mapName;
               this.mapSizeX = mapSizeX;
                this.mapSizeY = mapSizeY;
            }
        }

        public object CaptureState()
        {
            SaveInfo saveInfo = new SaveInfo(mapName, mapSize.x,mapSize.y);

            return saveInfo;
        }

        public void RestoreState(object state)
        {
            SaveInfo savedInfo = (SaveInfo)state;
            mapName = savedInfo.mapName;
            mapSize.x = savedInfo.mapSizeX;
            mapSize.y = savedInfo.mapSizeY;
        }
    }


}
