using System.Collections;
using System.Collections.Generic;
using System;
[Serializable]
public class InputEntry 
{
    public bool inGame;
    public string scenarioName;
    public float endingValue;
    public List<string> questionAnsered;

    public InputEntry(bool ingame, Scenario scenario,float value, List<string> questions)
    {
        questionAnsered = new List<string>(); //Init
        inGame = ingame;
        scenarioName = scenario.ToString();
        endingValue = value;
        questionAnsered.AddRange(questions);
        
    }
    

}
