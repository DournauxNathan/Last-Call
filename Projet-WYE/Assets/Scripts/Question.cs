using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class Question : ScriptableObject
{
    public List<QuestionData> question;
}

[System.Serializable]
public class QuestionData
{
    public string reference;
    [Space(5)]
    public string text;
    public List<Material> linkObjects;
    public AudioClip voices;
}
