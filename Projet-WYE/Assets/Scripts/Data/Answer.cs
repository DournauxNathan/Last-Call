using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Answer", menuName = "Scenario/Answer", order = 1)]
public class Answer : ScriptableObject
{
    public FormData type;
    public int id;
    public Keyword[] keywords;
}

[System.Serializable]
public class Keyword
{
    public string proposition;
    public bool isCorrectAnswer;
}
