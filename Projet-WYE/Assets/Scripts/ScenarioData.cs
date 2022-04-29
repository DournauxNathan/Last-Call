using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Scenario", menuName = "New Scenario", order = 0)]
public class ScenarioData : ScriptableObject
{
    public Scenario scenario;
    public List<Unit> units;
    public List<Answer> answers;
    public List<Question> questions;

    public Caller callerInformations;

    public Settings callSettings;

    public AudioClip dialogues;
    public float timeAfterDialogueBegins;
}

[System.Serializable]
public struct Caller
{
    public string name;
    public string age;

    public string newsPapersData;
}

[System.Serializable]
public struct Settings
{
    public float callDuration;

    public float timeBeforeCall;

    public float timeInImaginary;
}
