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
    public List<string> combineWith = new List<string>(); /*A changer par un outil qui vient lire ses combinaisons ????*/
    public int influence;

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

        combineWith.Add(entry[3]);

        influence = int.Parse(entry[4]);
        
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

public enum StateMobility
{
    Static,
    Dynamic,
}
