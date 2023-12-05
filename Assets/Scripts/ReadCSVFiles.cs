using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCSVFiles : MonoBehaviour
{
    public List<string> questTemplates;
    public List<string> questItems_Text;
    public List<string> questItems_Descriptions;
    // Start is called before the first frame update
    private void Start()
    {
        GenerateNewListFromCSV("Exerbike CSVs - Templates", questTemplates);
        GenerateNewListFromCSV("Exerbike CSVs - Packages", questItems_Text);

        //TODO: update "Destinations" with waypoints & coordinates
        GenerateNewListFromCSV("Exerbike CSVs - Destinations", questItems_Descriptions);
    }
    

    void GenerateNewListFromCSV(string csvName, List<string> outputList)
    {
        // Loading the dataset from Unity's Resources folder
        var dataset = Resources.Load<TextAsset>("CSVFiles/" + csvName);

        // Splitting the dataset in the end of line
        var splitDataset = dataset.text.Split(new char[] { '\n' });

        outputList.AddRange(splitDataset);
    }

    void MakeNewQuest()
    {
        int randT = Random.Range(0, questTemplates.Count);
        int randI_T = Random.Range(0, questItems_Text.Count);
        int randI_D = Random.Range(0, questItems_Descriptions.Count);

        string newQuest = string.Format(questTemplates[randT], questItems_Text[randI_T], questItems_Descriptions[randI_D]);
        Debug.Log(newQuest);
    }
}
