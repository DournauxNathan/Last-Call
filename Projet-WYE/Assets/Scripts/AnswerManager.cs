using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : Singleton<AnswerManager>
{
    public List<GameObject> age;
    public List<GameObject> adress;
    public MyDictionary<int, GameObject> Situations;
}
