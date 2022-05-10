using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : Singleton<AnswerManager>
{
    public List<GameObject> age;
    public List<GameObject> adress;
    public List<Situation> situations;

    [HideInInspector] public bool ageIsAnswered;
    [HideInInspector] public bool adressIsAnswer;
    [HideInInspector] public bool situationIsAnswer;

    public void DisableGOIn(List<GameObject> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i].SetActive(false);
        }
    }
}

[System.Serializable]
public struct Situation
{
    public int id;
    public List<GameObject> canvas;
}

