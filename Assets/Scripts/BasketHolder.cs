using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketHolder : MonoBehaviour
{
    [SerializeField] GameObject[] spawnpoints;

    public Transform GrabRandomBasketSpawnpoint()
    {
        int spawnNum = Random.Range(0, spawnpoints.Length);
        return spawnpoints[spawnNum].transform;
    }
}
