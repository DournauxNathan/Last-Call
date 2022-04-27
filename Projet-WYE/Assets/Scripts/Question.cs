using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New uestion", menuName = "Scenario/Question", order = 2)]
public class Question : ScriptableObject
{
    public List<QuestionData> questions;
}

[System.Serializable]
public class QuestionData
{
    public string question;
    public string answer;
    public AudioClip voices;
    public List<Material> linkObjects;
}
