using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinableObject_Data : MonoBehaviour
{
    private new string name;
    [Header("Data")]
    public int iD;
    public StateMobility state;
    public string combineWith;
    public int influence;

    [Header("Outline")]
    public Outline outline;
    public Material selectOutline;

    public void Init(string[] entry)
    {
        this.name = entry[0];
        iD = int.Parse(entry[1]);

        if (entry[2].Contains("STATIQUE"))
        {
            state = StateMobility.Static;
        }
        else if (entry[2].Contains("DYNAMIQUE"))
        {
            state = StateMobility.Dynamic;
        }

        combineWith = entry[3];
        influence = int.Parse(entry[4]);

        selectOutline = Resources.Load<Material>("Materials/Select Outline");
    }
}

public enum StateMobility
{
    Static,
    Dynamic,
}
