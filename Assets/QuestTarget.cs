using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public GameObject questItem;

    public void Init( GameObject newQuestItem)
    {
        questItem = newQuestItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("QuestItem"))// && other.gameObject == questItem)
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
