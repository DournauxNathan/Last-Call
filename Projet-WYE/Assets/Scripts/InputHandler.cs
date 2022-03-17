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
        
        //Attributing value
        MasterManager.Instance.isTutoEnded = entries[0].inGame;
        ScenarioManager.Instance.currentScenario = (Scenario)Enum.Parse(typeof(Scenario), entries[0].scenarioName,true);
        ScenarioManager.Instance.endingValue = entries[0].endingValue;
        MasterManager.Instance.currentPhase = (Phases)Enum.Parse(typeof(Phases), entries[0].currentPhase, true);
        SaveQuestion.Instance.AnsweredQuestions.AddRange(entries[0].questionAnsered);
        
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

    
}
