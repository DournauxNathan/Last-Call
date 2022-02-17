using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(Outline), typeof(SphereCollider))]
public class CombinableObject : CombinableObject_Data
{
    //public OrderFormat resultOrder;

    public bool isLocked = false;

    //public List<Combinaisons> combinaisons;
    //public List<GameObject> subList; //???? What is it ? Can't Remember ?

    // Start is called before the first frame update
    void Start()
    {
        SetOutline();

        //subList.Add(gameObject);   

        if (MasterManager.Instance.objectActivator != null)
        {
            if (MasterManager.Instance.objectActivator.objectByIdList.ContainsKey(iD))
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

    public void ToggleOutline(bool b)
    {
        if (!isLocked)
        {
            outline.enabled = b;
        }
        else
        {
            outline.enabled = true;
        }
    }

    public void Lock(bool b)
    {
        if (MasterManager.Instance.isInImaginary && b)
        {
            isLocked = true;

            outline.OutlineColor = selectOutline.color;
        }
        else if (MasterManager.Instance.isInImaginary && !b)
        {
            isLocked = false;

            outline.OutlineColor = defaultOutlineColor;

            ToggleOutline(b);
        }
    }

    //Old Code
    /*public void EnableOutline()
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
    */

    /* public void Locked()
     {
         if (MasterManager.Instance.isInImaginary)
         {
             isLocked = true;

             outline.OutlineColor = selectOutline.color;
         }
     }

     public void UnLocked()
     {
         if (MasterManager.Instance.isInImaginary)
         {
             isLocked = false;

             outline.OutlineColor = defaultOutlineColor;

             ToggleOutline();
         }
     }*/
}