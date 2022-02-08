using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(CombinableObject), typeof(Outline), typeof(SphereCollider))]
public class CombinableObject : CombinableObject_Data
{
    [Header("Outline")]
    private Outline outline;

    public OrderFormat resultOrder;

    [HideInInspector] public bool isLocked = false;

    public List<Combinaisons> combinaisons;
    //public List<GameObject> subList; //???? What is it ? Can't Remember ?

    private Color baseColor;
    private Color selectColor;
            
    // Start is called before the first frame update
    void Start()
    {
        SetTriggerCollider(true);

        //subList.Add(gameObject);   

        if (MasterManager.Instance.objectActivator != null)
        {
            if(MasterManager.Instance.objectActivator.objectByIdList.ContainsKey(iD) )
            {
                List<GameObject> tempObject;

                tempObject = MasterManager.Instance.objectActivator.objectByIdList[iD];
                //subList.AddRange(tempObject);
                MasterManager.Instance.objectActivator.objectByIdList.Remove(iD);
                //init.objectByIdList.Add(data.iD, subList);
                return;
            }/*
            else
            {
                init.objectByIdList.Add(data.iD, subList);
            }*/
        }
    }

    public void SetOutline()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        baseColor = outline.OutlineColor;
        selectColor = selectOutline.color;
    }

    private void SetTriggerCollider(bool newState)
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
            Debug.Log(this.gameObject.name + " | " + other.name);
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

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void DisabledOutline()
    {
        if (!isLocked)
        {
            outline.enabled = false;
        }
    }

    public void Locked()
    {
        if (MasterManager.Instance.isInImaginary)
        {
            isLocked = true;

            outline.OutlineColor = selectColor;
        }
    }

    public void UnLocked()
    {
        if (MasterManager.Instance.isInImaginary)
        {
            isLocked = false;

            outline.OutlineColor = baseColor;

            DisabledOutline();
        }
    }
}

[Serializable]
public class Combinaisons
{
    public GameObject combineWith;    
}