using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Answer", menuName = "Answer", order = 1)]
public class Answer : ScriptableObject
{
    public int numberOfProposition;
    public Keyword[] keywords;
}

[System.Serializable]
public class Keyword
{
    public string proposition;
    public bool isCorrectAnswer;
}
