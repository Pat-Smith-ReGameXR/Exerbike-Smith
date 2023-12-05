using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //Debug.Log("Fly script added to: " + gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Input.GetAxis("Vertical") * 1.2f, Input.GetAxis("Horizontal"), 0f);

        if (Input.GetButton("Fire1"))
            transform.position += transform.forward * Time.deltaTime * 75.0f;
    }
}
