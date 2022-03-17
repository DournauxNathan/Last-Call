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
    public MeshFilter meshFilter { get => m_MeshFilter; set => m_MeshFilter = value; }

    private MeshRenderer m_MeshRenderer;
    public MeshRenderer meshRenderer { get => m_MeshRenderer; set => m_MeshRenderer = value; }
    
    private MeshCollider m_MeshCollider;
    public MeshCollider meshCollider { get => m_MeshCollider; set => m_MeshCollider = value; }
    
    private SphereCollider m_Spherecollider;
    public SphereCollider sphereCollider { get => m_Spherecollider; set => m_Spherecollider = value; }
    
    private DissolveEffect m_DissolveEffect;
    public DissolveEffect dissolveEffect { get => m_DissolveEffect; set => m_DissolveEffect = value; }

    [Header("Outline Properties")]
    public Outline outline;
    public Material selectOutline;
    public Color defaultOutlineColor;

    public void GetComponent()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        sphereCollider = GetComponent<SphereCollider>();
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
                influence = int.Parse(entry[6]),
                outcome = entry[7]
                
            };
        }
        else if (nCombinaison == 2)
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6]),
                outcome = entry[7]
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[8],
                influence = int.Parse(entry[9]),
                outcome = entry[10]
            };
        }
        else if (nCombinaison == 3)
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6]),
                outcome = entry[7]
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[8],
                influence = int.Parse(entry[9]),
                outcome = entry[10]
            };

            useWith[2] = new CombineWith
            {
                objectName = entry[11],
                influence = int.Parse(entry[12]),
                outcome = entry[13]
            };
        }
        else
        {
            useWith[0] = new CombineWith
            {
                objectName = entry[5],
                influence = int.Parse(entry[6]),
                outcome = entry[7]
            };

            useWith[1] = new CombineWith
            {
                objectName = entry[8],
                influence = int.Parse(entry[9]),
                outcome = entry[10]
            };

            useWith[2] = new CombineWith
            {
                objectName = entry[11],
                influence = int.Parse(entry[12]),
                outcome = entry[13]
            };

            useWith[3] = new CombineWith
            {
                objectName = entry[14],
                influence = int.Parse(entry[15]),
                outcome = entry[16]
            };
        }

        LoadFromRessources();
        SetOutline();
        SetCollider();
    }

    public void LoadFromRessources()
    {
        meshFilter.mesh = Resources.Load<Mesh>("Models/" + name);
        meshRenderer.materials = Resources.LoadAll<Material>("Materials/" + name + "/M_" + name);
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
    public string outcome;
}

public enum StateMobility
{
    Static,
    Dynamic,
}
