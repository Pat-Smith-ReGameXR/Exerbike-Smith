using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEffectsSpawner : MonoBehaviour
{
    [Range(4, 12)]
    [SerializeField] private int spawnNumRange;
    //radius on which to spawn characters
    public float radius;

    public GameObject[] spawnedObjectArray;

    List<Vector3> spawnLocations = new List<Vector3>();
    List<Vector3> testLocations = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        CheckSpawnLocations();
        CreateSpawnedInstances();
    }

    void CheckSpawnLocations()
    {
        /*float modX = 0, modZ = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) continue;

                modX = radius * (x != 0 ? x : 0.5f * z);
                modZ = radius * (z != 0 ? z : 0.5f * x);

                Vector3 origPos = transform.position;
                Vector3 spawnLocMod = new Vector3(modX, 0, modZ);


                spawnLocations.Add(origPos + spawnLocMod);
            }
        }
        */

        for (int i = 0; i <= spawnNumRange; i++)
        {
            int findNewSpotCycles = 20;

            for (int j = 0; j < findNewSpotCycles; j++)
            {
                Vector3 checkInsideRadius = Random.insideUnitSphere;// .Normalize() * radius;
                checkInsideRadius.Normalize();
                checkInsideRadius *= radius;

                //Random.insideUnitCircle is a Vector2 - this converts the value into a proper Vector3
                checkInsideRadius = new Vector3(checkInsideRadius.x, 0, checkInsideRadius.y);

                Vector3 origPos = transform.position;
                Vector3 newSpawnPos = origPos + checkInsideRadius;
                //Debug.Log(newSpawnPos);

                //now, with a random point, shoot down a Raycast to make sure the point is on a sidewalk, not the street/building 
                RaycastHit hit;

                //Debug.Log("CHECKPOINT 1: " + Physics.Raycast(newSpawnPos + (Vector3.up * 3), -Vector3.up, out hit, 30f));
                //Debug.Log("CHECKPOINT 2: " + hit.collider != null);
                //Debug.Log("CHECKPOINT 3: " + hit.collider.gameObject.GetComponent<Renderer>().materials.Length);
                //Debug.Log("CHECKPOINT 4: " + hit.collider.gameObject.GetComponent<Renderer>().materials[0].name);

                if (Physics.Raycast(newSpawnPos + (Vector3.up * 3), -Vector3.up, out hit, 30f) && hit.collider != null && 
                    (hit.collider.gameObject.GetComponent<Renderer>().materials.Length > 0 && 
                    hit.collider.gameObject.GetComponent<Renderer>().materials[0].name == "material1t10gpc63142te (Instance)"))
                //TODO: change this to the appropriate material that is used on the sidewalk!!
                //TODO: this logic will only apply IF the first material for the sidewalk is the "material1t10gpc63142te" material
                {
                    //Debug.Log("VALID SPOT FOUND - ADDING TO SPAWN LIST...");
                    spawnLocations.Add(newSpawnPos);
                    break;
                }
                else
                {
                    //Debug.Log("NO VALID SPOT FOUND - RE-DOING SPAWN...");
                    testLocations.Add(newSpawnPos);
                }

            }
        }
    }

    //run through this function AFTER you have found enough viable spawn locations
    void CreateSpawnedInstances()
    {
        int randNPC = 0;
        GameObject newSpawn;

        foreach (Vector3 newSpawnLoc in spawnLocations)
        {
            randNPC = Random.Range(0, spawnedObjectArray.Length);
            newSpawn = Instantiate(spawnedObjectArray[randNPC], newSpawnLoc, Quaternion.identity);

            newSpawn.transform.parent = this.transform;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        foreach (Vector3 testLoc in testLocations)
        {
            Gizmos.DrawWireSphere(testLoc, 0.3f);
            Gizmos.DrawLine(testLoc + (Vector3.up * 3), testLoc);
        }
    }

}
