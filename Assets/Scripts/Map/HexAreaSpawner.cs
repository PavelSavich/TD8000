using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Map
{
    public class HexAreaSpawner : MonoBehaviour
    {
        [SerializeField] Vector3 spread = new Vector3();
        [SerializeField] float overlapBoxSize = 1f;

        public void SpawnObjects(GameObject objectToSpawn, int amountToSpawn, LayerMask spawnedObjectLayer, GameObject parentOfSpawned)
        {
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

                Debug.Log("object spawned");

                Vector3 overlapBoxScale = new Vector3(overlapBoxSize, 0, overlapBoxSize);

                Collider[] collidersInsideOverlapBox = new Collider[2];

                int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(spawnedObject.transform.position,
                                                                        overlapBoxScale,
                                                                        collidersInsideOverlapBox,
                                                                        spawnedObject.transform.rotation,
                                                                        spawnedObjectLayer);

                Debug.Log("number of colliders found " + numberOfCollidersFound);

                if (numberOfCollidersFound > 1)
                {
                    Destroy(spawnedObject.gameObject);
                    //objectsSpawned--;
                    Debug.Log("name of collider 0 found " + collidersInsideOverlapBox[0].name);
                }
            }
        }
    }
}
