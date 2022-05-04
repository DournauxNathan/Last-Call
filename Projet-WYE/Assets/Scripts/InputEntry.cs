using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class InputEntry 
{
    public bool inGame;
    public string scenarioName;
    public float endingValue;
    public string currentPhase;
    public List<string> questionAnsered;

    public InputEntry(bool ingame, Scenario scenario,float value,Phases phase , List<string> questions)
    {
        questionAnsered = new List<string>(); //Init
        inGame = ingame;
        scenarioName = scenario.ToString();
        endingValue = value;
        currentPhase = phase.ToString();
        questionAnsered.AddRange(questions);
        
    }
}
