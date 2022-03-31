using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Answer", menuName = "Answer", order = 0)]
public class Answer : ScriptableObject
{
    public Keyword[] keywords;
    public AudioClip voices;
}

[System.Serializable]
public class Keyword
{
    public string proposition;
    public bool isCorrectAnswer;
}
