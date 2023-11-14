using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmarkAutoScripts : MonoBehaviour
{
    [SerializeField] Vector2[] landmarkCoordinatesArray;
    [SerializeField] string[] landmarkNames;

    List<MeshRenderer> meshRendList = new List<MeshRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        meshRendList.AddRange(GameObject.FindObjectsOfType<MeshRenderer>());
        Debug.Log("MESH REND LIST: " + meshRendList.Count);

        MeshCollider mColHolder;
        foreach (MeshRenderer mRend in meshRendList)
        {
            mColHolder = mRend.gameObject.AddComponent<MeshCollider>();
            mColHolder.sharedMesh = mRend.gameObject.GetComponent<MeshFilter>().mesh;
            mColHolder.convex = true;
        }

        landmarkNames = new string[landmarkCoordinatesArray.Length];

        for (int i = 0; i < landmarkCoordinatesArray.Length; i++)
        {
            landmarkNames[i] = GetLandmarkName(landmarkCoordinatesArray[i]);
        }
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

    /*GameObject FindLandmark(Vector3 coords)
    {
        {
            Vector3 currentPosition = coords;

            MeshRenderer closestComponent = ComponentSearcher<MeshRenderer>.Closest(currentPosition, meshRendList);
            //MeshRenderer furthestComponent = ComponentSearcher<MeshRenderer>.Furthest(currentPosition, radius);
            closestComponent.gameObject.name = "ABCD";
            Debug.Log("CLOSEST TEST: " + closestComponent + "/");

            return closestComponent.gameObject;

            //float searchRadius = 100f;
            //List<MeshRenderer> componentsInRange = new List<MeshRenderer>();
            //ComponentSearcher<MeshRenderer>.InRadius(currentPosition, searchRadius, out componentsInRange);

            //Debug.Log("COMPONENTS: " + componentsInRange.Count);
            //Debug.Log("NEAREST: " + closestComponent.gameObject.name);

            // Now you can interact with the found components
        }
    }*/

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Vector2 coords in landmarkCoordinatesArray)
        {
            Gizmos.DrawLine(ConvertToVector3(coords), ConvertToVector3(coords, 0));
        }
    }

}
