using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerAudioClip : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] float souvenirScaling = .002f;
    [SerializeField] Vector3 buildingLocation;

    private void Start()
    {
        //TODO: find a better way to grab the game's main AudioSource
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    public void SetAudioClip(AudioClip newClip)
    {
        audioClip = newClip;
    }

    public void SetLocationInfo(Vector3 location){ buildingLocation = new Vector3(location.x, 20f, location.z);}

    public Vector3 LandmarkLocation { get { return buildingLocation; } }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();

                SpawnBuildingInBasket(other.gameObject, this.gameObject);
                StartCoroutine(ChangeBuildingColor(audioClip));

            }
        }      
    }

    public void SpawnBuildingInBasket(GameObject player, GameObject building)
    {
        GameObject newSouvenir = Instantiate(building.gameObject, gameObject.transform.position, Quaternion.identity);
        newSouvenir.transform.eulerAngles = new Vector3(-90, 0, 0);
        newSouvenir.name = "DUPLICATE LANDMARK";

        GameObject newSouvenirHolder = new GameObject();// (GameObject)Instantiate(null, buildingLocation, Quaternion.identity);
        newSouvenirHolder.transform.position = buildingLocation;
        newSouvenirHolder.name = "SOUVENIR";

        newSouvenir.transform.parent = newSouvenirHolder.transform;      

        Transform newSpawnTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<BasketHolder>().GrabRandomBasketSpawnpoint();
        newSouvenirHolder.transform.parent = newSpawnTransform;
        newSouvenirHolder.transform.localPosition = Vector3.zero;
        newSouvenirHolder.transform.localEulerAngles = Vector3.zero;
        newSouvenirHolder.transform.localScale = Vector3.one * souvenirScaling;
        
    }

    IEnumerator ChangeBuildingColor(AudioClip audioClip)
    {
        foreach (MeshRenderer childMaterial in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            Material objectMaterial = childMaterial.material;
            objectMaterial.color = Color.blue;
        }

        yield return new WaitForSeconds(audioClip.length);

        foreach (MeshRenderer childMaterial in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            Material objectMaterial = childMaterial.material;
            objectMaterial.color = Color.white;
        }
    }
}
