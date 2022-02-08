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

    public Material selectOutline;

    [Header("Refs")]
    public MeshFilter m_MeshFilter;
    public MeshRenderer m_MeshRenderer;
    public MeshCollider m_MeshCollider;
    public SphereCollider m_Spherecollider;

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

        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshCollider = GetComponent<MeshCollider>();
        m_Spherecollider = GetComponent<SphereCollider>();

        m_MeshFilter.mesh = Resources.Load<Mesh>("Models/" + name);
        m_MeshRenderer.materials = Resources.LoadAll<Material>("Materials/" + name + "/M_" + name);
        
        selectOutline = Resources.Load<Material>("Materials/Select Outline");


    }
}

public enum StateMobility
{
    Static,
    Dynamic,
}
