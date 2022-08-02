using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class HexAreaSpawner : MonoBehaviour
    {
        [SerializeField] bool canSpawn = true;

        public void DeactivateSpawner()
        {
            canSpawn = false;
        }

        public bool CanSpawn()
        {
            return canSpawn;
        }

        public void SpawnObjects(GameObject objectToSpawn, int amountToSpawn,Vector3 spread,float overlapBoxSize, LayerMask spawnedObjectLayer, GameObject parentOfSpawned)
        {
            if (!canSpawn) { return; }

            for (int objectsSpawned = 0; objectsSpawned < amountToSpawn; objectsSpawned++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-spread.x, spread.x),
                                                     Random.Range(-spread.y, spread.y),
                                                     Random.Range(-spread.z, spread.z))
                                                     + transform.position;

                GameObject spawnedObject = Instantiate(objectToSpawn, 
                                                       randomPosition, 
                                                       objectToSpawn.transform.rotation, 
                                                       parentOfSpawned.transform);

                Vector3 overlapBoxScale = new Vector3(overlapBoxSize, 0, overlapBoxSize);

                Collider[] collidersInsideOverlapBox = new Collider[2];

                int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(spawnedObject.transform.position,
                                                                        overlapBoxScale,
                                                                        collidersInsideOverlapBox,
                                                                        spawnedObject.transform.rotation,
                                                                        spawnedObjectLayer);
                if (numberOfCollidersFound > 1)
                {
                    Destroy(spawnedObject.gameObject);
                    //TODO: inplement objectsSpawned--;
                }
            }
        }
    }
}
