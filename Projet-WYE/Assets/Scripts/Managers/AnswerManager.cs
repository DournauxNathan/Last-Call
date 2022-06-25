using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : Singleton<AnswerManager>
{
    public List<GameObject> age;
    public List<GameObject> adress;
    public List<Situation> situations;

    public bool ageIsAnswered;
    public bool adressIsAnswer;
    public bool situationIsAnswer;

    public void DisableGOIn(List<GameObject> t)
    {
        foreach (var item in t)
        {
            item.GetComponent<WordData>().Deactivate();
        }
        /*
        for (int i = 0; i < t.Count; i++)
        {
            t[i].SetActive(false);
            //t[i].GetComponent<WordData>().Deactivate();
        }*/
    }
}

[System.Serializable]
public struct Situation
{
    public int id;
    public List<GameObject> canvas;
}

