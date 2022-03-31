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
    public string text;
    public AudioClip voices;
    public List<Material> linkObjects;
}
