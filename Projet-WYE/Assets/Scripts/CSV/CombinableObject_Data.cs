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
    private MeshRenderer m_MeshRenderer;
    private MeshCollider m_MeshCollider;
    private SphereCollider m_Spherecollider;

    [Header("Outline Properties")]
    public Outline outline;
    public Material selectOutline;
    public Color defaultOutlineColor;

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
        m_MeshFilter = GetComponent<MeshFilter>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshCollider = GetComponent<MeshCollider>();
        m_Spherecollider = GetComponent<SphereCollider>();
        outline = GetComponent<Outline>();
        #endregion

        LoadFromRessources();
        SetOutline();
        SetCollider();
    }

    public void LoadFromRessources()
    {
        m_MeshFilter.mesh = Resources.Load<Mesh>("Models/" + name);
        m_MeshRenderer.materials = Resources.LoadAll<Material>("Materials/" + name + "/M_" + name);
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
