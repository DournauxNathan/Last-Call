using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Scenario", menuName = "New Scenario", order = 0)]
public class ScenarioData : ScriptableObject
{
    public Scenario scenario;

    public string callerName;

    public List<Answer> answers;
    public List<Question> questions;
}
