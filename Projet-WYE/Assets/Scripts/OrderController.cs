using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OrderController : Singleton<OrderController>
{
    public int currentNumberOfCombinaison;
    public int numberOfCombinaison;
    public List<OrderFormat> orders;
    public bool isResolve = false;

    public void Setup()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("ObjCombi");

        ObjectActivator.Instance.SetActivetObject(go);
        numberOfCombinaison = ObjectActivator.Instance.indexesList.Count / 2;
    }
    public int IncreaseValue(int _value)
    {        
        currentNumberOfCombinaison += _value;        
        Resolve();
        return currentNumberOfCombinaison;
    }
    public void Resolve()
    {
        if (currentNumberOfCombinaison == numberOfCombinaison)
        {
            MasterManager.Instance.isInImaginary = false;
            isResolve = true;
            SceneLoader.Instance.LoadNewScene("Office");            
        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }
}
