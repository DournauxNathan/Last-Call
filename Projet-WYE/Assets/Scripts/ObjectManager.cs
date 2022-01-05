using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Outline), typeof(SphereCollider))]
public class ObjectManager : MonoBehaviour
{
    public ObjectType Objecttype;
    public ObjectData data;
    public Combinaisons[] combinaisons;
    public List<GameObject> subList; //???? What is it ? Can't Remember ?

    [HideInInspector] public Outline outline;
    [HideInInspector] public Color baseColor;
    [HideInInspector] public Color selectColor;
    
    private ObjectActivator init;

    private void Awake()
    {
        SetTriggerColliDe(true);
        SetOutline();
    }

    // Start is called before the first frame update
    void Start()
    {
        subList.Add(gameObject);   

        if (GameObject.Find("ObjetAactiver") != null)
        {
            init = GameObject.Find("ObjetAactiver").GetComponent<ObjectActivator>();        

            if(init.objectByIdList.ContainsKey(data.iD) )
            {
                List<GameObject> tempObject;

                tempObject = init.objectByIdList[data.iD];
                subList.AddRange(tempObject);
                init.objectByIdList.Remove(data.iD);
                init.objectByIdList.Add(data.iD, subList);
                return;
            }
            else
            {
                init.objectByIdList.Add(data.iD, subList);
            }
        }
    }

    private void SetOutline()
    {
        if (outline == null)
        {
            outline = GetComponent<Outline>();
        }

        outline.enabled = false;

        baseColor = GetComponent<Outline>().OutlineColor;
        selectColor = data.selectOutline.color;
    }

    private void SetTriggerColliDe(bool newState)
    {
        if (GetComponents<SphereCollider>().Length == 2)
        {
            GetComponents<SphereCollider>()[1].isTrigger = newState;
        }
        else
        {
            GetComponent<SphereCollider>().isTrigger = newState;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjCombi"))
        {
            ListManager.Instance.CheckCompatibility(this.gameObject, other.gameObject);
        }

        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Disable hand colliDer");
            other.GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Enable hand colliDer");
            other.GetComponent<MeshCollider>().enabled = true;
        }
    }


    public void Enable()
    {
        outline.enabled = true;
    }

    public void Disabled()
    {
        if (!data.isLocked)
        {
            outline.enabled = false;
        }
    }

    public void Locked()
    {
        if (GameObject.FindObjectOfType<ObjectActivator>().inImaginaire)
        {
            data.isLocked = true;
            outline.OutlineColor = selectColor;
        }
    }

    public void UnLocked()
    {
        if (MasterManager.Instance.isInImaginary)
        {
            data.isLocked = false;
            outline.OutlineColor = baseColor;
            Disabled();
        }
    }
}

[Serializable]
public class Combinaisons
{
    public List<GameObject> combineWith = new List<GameObject>();    
}

[Serializable]
public class ObjectData
{
    public int iD;
    public bool isStatic;
    public OrderFormat resultOrder;
    public Material selectOutline;

    [HideInInspector] public bool isLocked = false;
}

public enum ObjectType
{
    Useful,
    Useless
}