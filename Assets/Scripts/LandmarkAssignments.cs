using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LandmarkAssignments : MonoBehaviour
{
    [SerializeField] BasketHolder bhScript;
    [SerializeField] LandmarkInfoSO[] lmInfoList;

    List<MeshRenderer> meshRendList = new List<MeshRenderer>();

    public GameObject testObj;

    // Start is called before the first frame update
    void Start()
    {
        lmInfoList = Resources.LoadAll<LandmarkInfoSO>("LandmarkInfo");

        GPSEncoder.SetLocalOrigin(new Vector2(40.814762f, -74.003571f));
        Instantiate(testObj, GPSEncoder.GPSToUCS(new Vector2(40.814762f, -74.003571f)), Quaternion.identity);
        Instantiate(testObj, GPSEncoder.GPSToUCS(40.800558f, -73.958174f), Quaternion.identity);
        Instantiate(testObj, GPSEncoder.GPSToUCS(40.76764238564256f, -73.97130251422423f), Quaternion.identity);

        meshRendList.AddRange(GameObject.FindObjectsOfType<MeshRenderer>());
        Debug.Log("MESH REND LIST: " + meshRendList.Count);

        MeshCollider mColHolder;
        foreach (MeshRenderer mRend in meshRendList)
        {
            mColHolder = mRend.gameObject.AddComponent<MeshCollider>();
            mColHolder.sharedMesh = mRend.gameObject.GetComponent<MeshFilter>().mesh;
            //mColHolder.convex = true;
        }

        CreateNewLandmarks();
    }

    string GetLandmarkName(Vector2 coordinates)
    {
        RaycastHit hit;
        if (Physics.Raycast(ConvertToVector3(coordinates), -Vector3.up, out hit, Mathf.Infinity) && hit.collider != null)
        {
            GameObject foundObject = hit.collider.gameObject;

            while (foundObject.transform.parent.parent != null)
            {
                Debug.Log(foundObject.transform.parent);
                foundObject = foundObject.transform.parent.gameObject;
            }

            return foundObject.name;
        }

        //FindLandmark(ConvertToVector3(coordinates));

        return "NULL";
    }

    Vector3 ConvertToVector3(Vector2 vec2, float yVal = 500f)
    {
        return new Vector3(vec2.x, yVal, vec2.y);
    }

    void CreateNewLandmarks()
    {
        GameObject newLandmark = null;

        foreach (LandmarkInfoSO landmark in lmInfoList)
        {
            newLandmark = null;
            Vector3 landmarkPos = landmark.GetLandmarkLocation();
            Debug.Log("POS ATTEMPT: " + landmarkPos);

            RaycastHit hit;
            if (Physics.Raycast(landmarkPos + (Vector3.up * 500), -Vector3.up, out hit, Mathf.Infinity) && hit.collider != null)
            {
                newLandmark = hit.collider.gameObject;

                if (newLandmark.transform.parent != null)
                {
                    while (newLandmark.transform.parent.parent != null)
                    {
                        newLandmark = newLandmark.transform.parent.gameObject;
                    }
                }
            }

            if (newLandmark == null) { Debug.LogError("ERROR: Landmark not found. Make sure that the coordinates are accurate and that there is a texture at that point."); continue; }

            newLandmark.name = landmark.LandmarkName;
            CreateNewAudioTrigger(newLandmark, landmark);
        }
    }

    void CreateNewAudioTrigger(GameObject landmark, LandmarkInfoSO landmarkInfo)
    {
        SphereCollider newTrigger = landmark.AddComponent<SphereCollider>();

        newTrigger.radius = 40f;
        newTrigger.isTrigger = true;

        TriggerAudioClip newTriggerScript = landmark.AddComponent<TriggerAudioClip>();
        newTriggerScript.SetAudioClip(landmarkInfo.LandmarkDescAudio);
        newTriggerScript.SetLocationInfo(landmarkInfo.GetLandmarkLocation());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (LandmarkInfoSO landmark in lmInfoList)
        {
            Gizmos.DrawLine(ConvertToVector3(landmark.inGameVector2), ConvertToVector3(landmark.inGameVector2, 0));
        }
    }

}
