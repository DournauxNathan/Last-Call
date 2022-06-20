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
    public float ageBegin;
    public float adressBegin;
    public float situationBegin;

    public float timerInPhase2;

    public AudioClip conclusion;

    public List<AudioClip> monologue;
    public int numberOfPuzzle;
}

[System.Serializable]
public struct Caller
{
    public string name;
    public string age;
    public Question adress;

    public string newsPapersData;
}

[System.Serializable]
public struct Settings
{
    public float callDuration;

    public float timeBeforeCall;

    public float timeInImaginary;
}
