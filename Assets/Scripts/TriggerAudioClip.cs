using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerAudioClip : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject[] basketSpawnpoints;
    List<int> availableSpawns = new List<int>();
    [SerializeField] float souvenirScaling = 1f;

    private void Start()
    {
        //TODO: find a better way to grab the game's main AudioSource
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        for(int i = 0; i < basketSpawnpoints.Length; i++)
        {
            availableSpawns.Add(i);
        }
    }

    public void SetAudioClip(AudioClip newClip)
    {
        audioClip = newClip;
    }

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
        GameObject newSouvenir = Instantiate(building.gameObject, Vector3.zero, Quaternion.identity);
        newSouvenir.transform.parent = player.transform;
        newSouvenir.transform.localScale = Vector3.one * souvenirScaling;
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
