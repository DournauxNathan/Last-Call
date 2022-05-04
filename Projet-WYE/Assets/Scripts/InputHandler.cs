using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class  InputHandler : MonoBehaviour
{
    [SerializeField]List<InputEntry> entries = new List<InputEntry>();
    [SerializeField] private string filename;

    public void LoadSave()
    {
        entries = FileHandler.ReadFromJSON<InputEntry>(filename);

        if (SecurityCheckSave(entries))
        {
            MasterManager.Instance.isTutoEnded = entries[0].inGame;
            ScenarioManager.Instance.currentScenario = (Scenario)Enum.Parse(typeof(Scenario), entries[0].scenarioName, true);
            ScenarioManager.Instance.endingValue = entries[0].endingValue;
            MasterManager.Instance.currentPhase = (Phases)Enum.Parse(typeof(Phases), entries[0].currentPhase, true);
            SaveQuestion.Instance.AnsweredQuestions.AddRange(entries[0].questionAnsered);
        }
        else
        {
            Debug.LogError("Try to Delete your save file at" + FileHandler.GetPath("SaveLastCall.json"));
        }
    }

    public string LoadSaveData()
    {
        entries = FileHandler.ReadFromJSON<InputEntry>(filename);
        string output = "";
        output = /*"Scénario en cours : " + entries[0].scenarioName.ToString() +*/ 
        "\n Jour : ";

        return output;
    }


    public void AddSave()
    {
        List<string> _string = new List<string>();
        _string.Add("");
        //Debug.Log(MasterManager.Instance.isTutoEnded.ToString() +" "+ ScenarioManager.Instance.currentScenario +" "+ ScenarioManager.Instance.endingValue +" "+ SaveQuestion.Instance.AnsweredQuestions);
        entries.Clear();
        if (SaveQuestion.Instance.AnsweredQuestions.Count !=0)
        {
            entries.Add(new InputEntry(MasterManager.Instance.isTutoEnded, ScenarioManager.Instance.currentScenario, ScenarioManager.Instance.endingValue,MasterManager.Instance.currentPhase ,SaveQuestion.Instance.AnsweredQuestions));
        }
        else
        {
            entries.Add(new InputEntry(MasterManager.Instance.isTutoEnded, ScenarioManager.Instance.currentScenario, ScenarioManager.Instance.endingValue, MasterManager.Instance.currentPhase , _string));
        }
        

        FileHandler.SaveToJSON<InputEntry>(entries,filename);
    }

    private bool SecurityCheckSave(List<InputEntry> inputEntries)
    {
        bool returnValue = true;
        if (inputEntries[0].inGame.ToString() == "true"|| inputEntries[0].inGame.ToString() == "false")
        {
            //is a bool
        }
        else
        {
            Debug.LogError("File Corrupted");
            return false;
        }

        if (!Enum.IsDefined(typeof(Scenario), inputEntries[0].scenarioName))
        {
            Debug.LogError("File Corrupted");
            return false;
        }

        var a = inputEntries[0].endingValue;
        if (a.GetType() != typeof(int))
        {
            Debug.LogError("File Corrupted");
            return false;
        }

        if (!Enum.IsDefined(typeof(Phases),inputEntries[0].currentPhase))
        {
            Debug.LogError("File Corrupted");
            return false;
        }


        return returnValue; // a changer
    }
}
