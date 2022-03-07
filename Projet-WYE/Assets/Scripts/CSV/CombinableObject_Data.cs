using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombinableObject_Data : MonoBehaviour
{
    private bool generate;

    public new string name;
    [Header("Data")]
    public int iD;
    public StateMobility state;
    private int nCombinaison;
    public CombineWith[] useWith; 

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
        this.name = entry[1];

        iD = int.Parse(entry[2]);

        if (entry[3].Contains("STATIQUE"))
        {
            state = StateMobility.Static;
        }
        else if (entry[3].Contains("DYNAMIQUE"))
        {
            state = StateMobility.Dynamic;
        }

        nCombinaison = int.Parse(entry[4]);

        useWith = new CombineWith[nCombinaison];

        if (nCombinaison == 1)
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6])
            };
        }
        else if (nCombinaison == 2)
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6])
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[7],
                influence = int.Parse(entry[8])
            };
        }
        else if (nCombinaison == 3)
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6])
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[7],
                influence = int.Parse(entry[8])
            };

            useWith[2] = new CombineWith
            {
                objectName = entry[9],
                influence = int.Parse(entry[10])
            };
        }
        else
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6])
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[7],
                influence = int.Parse(entry[8])
            };

            useWith[2] = new CombineWith
            {
                objectName = entry[9],
                influence = int.Parse(entry[10])
            };

            useWith[3] = new CombineWith
            {
                objectName = entry[11],
                influence = int.Parse(entry[12])
            };
        }

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
