using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketHolder : MonoBehaviour
{
    [SerializeField] GameObject[] spawnpoints;
    [SerializeField] GameObject questItemSpawnPoint;

    [SerializeField] GameObject throwQuestItemPoint;
    [SerializeField] float throwForce = 30f;
    GameObject questItemToThrow;
    bool throwPrep = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && questItemSpawnPoint.transform.childCount > 0)
        {
            questItemToThrow = questItemSpawnPoint.transform.GetChild(0).gameObject;
            questItemToThrow.transform.parent = throwQuestItemPoint.transform;
            questItemToThrow.transform.localPosition = Vector3.zero;

            if (questItemToThrow.GetComponent<Rigidbody>() == null)
            {
                Rigidbody newRB = questItemToThrow.AddComponent<Rigidbody>();
                newRB.isKinematic = true;
            }

            throwPrep = true;
        }

        if (Input.GetButtonUp("Fire2") == true)
        {
            Rigidbody questItemRB = questItemToThrow.GetComponent<Rigidbody>();
            questItemRB.isKinematic = false;
            questItemRB.AddForce((transform.forward + transform.up) * throwForce);
            questItemRB.collisionDetectionMode = CollisionDetectionMode.Continuous;

            questItemToThrow.transform.parent = null;

            throwPrep = false;
        }
    }

    public Transform GrabRandomBasketSpawnpoint()
    {
        int spawnNum = Random.Range(0, spawnpoints.Length);
        return spawnpoints[spawnNum].transform;
    }

    public Transform GrabQuestItemSpawnpoint()
    {
        return questItemSpawnPoint.transform;
    }

}
