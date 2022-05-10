using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : Singleton<AnswerManager>
{
    public List<GameObject> age;
    public List<GameObject> adress;
    public List<Situation> situations;

}

[System.Serializable]
public struct Situation
{
    public int id;
    public List<GameObject> canvas;
}

