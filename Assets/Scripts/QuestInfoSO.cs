using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Custom Objects/Quest Info", order = 2)]
public class QuestInfoSO : ScriptableObject
{
    //TODO: make a list of variables that can change & influence the main quest -
    //the item to deliver, what the delivery point looks like, what the recipient
    //looks like, etc

    [SerializeField] GameObject package;
    public GameObject GetPackage { get { return package; } }

    [SerializeField] GameObject recipient;
    public GameObject GetRecipient { get { return recipient; } }

}
