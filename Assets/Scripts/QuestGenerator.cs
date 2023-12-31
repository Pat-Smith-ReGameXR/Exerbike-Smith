using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    public bool randomPos_Test = false;

    //TODO: when we can access waypoints, grab one of the waypoints (from the player's current position/
    //previously passed waypoint to the final waypoint) & pick a random one to spawn the quest starting
    //point (set variables so it's like forst 1/2 of the tour). After the player accesses the quest, 
    //THEN spawn the endpoint in one of the waypoints on the 2nd 1/2 of the trip

    public Vector3 pathStartPos;
    public Vector3 pathEndPos;

    public Vector3 questStartPos, questEndPos;

    public GameObject testObject;
    public GameObject questObject;

    public GameObject deliveryTarget;

    GameObject endQuestPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (randomPos_Test == true)
        {
            float dist = Vector3.Distance(pathEndPos, pathStartPos);
            Vector3 normDist = Vector3.Normalize(pathEndPos - pathStartPos);

            float randDist_Receive = Random.Range(5, 45);
            questStartPos = pathStartPos + ((randDist_Receive / 100f) * dist * normDist);

            float randDist_Complete = Random.Range(55, 95);
            questEndPos = pathStartPos + ((randDist_Complete / 100f) * dist * normDist);

            Instantiate(testObject, pathStartPos, Quaternion.identity);
            Instantiate(testObject, pathEndPos, Quaternion.identity);

            GameObject q1 = Instantiate(questObject, questStartPos, Quaternion.identity);
            q1.name = "Quest - StartPoint";
            GameObject q2 = Instantiate(questObject, questEndPos, Quaternion.identity);
            endQuestPoint = q2;
            q2.name = "Quest - EndPoint";

            StartCoroutine(Start_Part2());
        }
    }

    IEnumerator Start_Part2()
    {
        yield return new WaitForSeconds(1f);

        Vector3 randTargetSpawn = Random.insideUnitSphere * 20f;
        randTargetSpawn = Vector3.Scale(randTargetSpawn, new Vector3(1f, 0f, 1f));
        randTargetSpawn += endQuestPoint.transform.position;

        GameObject newDeliveryObject = Instantiate(deliveryTarget, randTargetSpawn, Quaternion.identity);
        newDeliveryObject.transform.rotation = Quaternion.LookRotation(endQuestPoint.transform.position - newDeliveryObject.transform.position);
        newDeliveryObject.transform.rotation = Quaternion.Euler(new Vector3(newDeliveryObject.transform.rotation.x - 30, newDeliveryObject.transform.rotation.y, newDeliveryObject.transform.rotation.z));
    }

}
