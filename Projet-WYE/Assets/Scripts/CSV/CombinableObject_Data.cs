using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinableObject_Data : MonoBehaviour
{
    public new string name;
    [Header("Data")]
    public int iD;
    public StateMobility state;
    private int nCombinaison;
    public List<CombineWith> useWith = new List<CombineWith>(); 

    [Header("Refs")]
    private MeshFilter m_MeshFilter;
    public MeshFilter MeshFilter { get => m_MeshFilter; set => m_MeshFilter = value; }

    private MeshRenderer m_MeshRenderer;
    public MeshRenderer MeshRenderer { get => m_MeshRenderer; set => m_MeshRenderer = value; }
    private MeshCollider m_MeshCollider;
    public MeshCollider MeshCollider { get => m_MeshCollider; set => m_MeshCollider = value; }
    private SphereCollider m_Spherecollider;
    public SphereCollider SphereCollider { get => m_Spherecollider; set => m_Spherecollider = value; }

    [Header("Outline Properties")]
    public Outline outline;
    public Material selectOutline;
    public Color defaultOutlineColor;


    public void GetComponent()
    {
        MeshFilter = GetComponent<MeshFilter>();
        MeshRenderer = GetComponent<MeshRenderer>();
        MeshCollider = GetComponent<MeshCollider>();
        SphereCollider = GetComponent<SphereCollider>();
        outline = GetComponent<Outline>();
    }
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

        nCombinaison = int.Parse(entry[3]);

        for (int i = 0; i < nCombinaison; i++)
        {
            useWith = new List<CombineWith>(nCombinaison);
            useWith[i].objectName = entry[i];
            useWith[i].influence = int.Parse(entry[i]);
        }




        #region Get Components

        #endregion

        LoadFromRessources();
        SetOutline();
        SetCollider();
    }

    public void LoadFromRessources()
    {
        MeshFilter.mesh = Resources.Load<Mesh>("Models/" + name);
        MeshRenderer.materials = Resources.LoadAll<Material>("Materials/" + name + "/M_" + name);
        selectOutline = Resources.Load<Material>("Materials/Select Outline");
    }

    public void SetOutline()
    {
        outline.enabled = false;
        defaultOutlineColor = outline.OutlineColor;
    }

    public void SetCollider()
    {
        if (GetComponents<SphereCollider>().Length == 2)
        {
            GetComponents<SphereCollider>()[1].isTrigger = true;
        }
        else
        {
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
}

[System.Serializable]
public class CombineWith
{
    public string objectName;
    public int influence;
}

public enum StateMobility
{
    Static,
    Dynamic,
}
