using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OrderController : Singleton<OrderController>
{
    public static OrderController instance;

    public int currentNumberOfCombinaison;
    public int numberOfCombinaison;
    public List<string> orders;

    public bool isResolve = false;

    public Transform parentOfResponses = null;
    public GameObject prefab_btnResponse;

    public void Setup()
    {
        if (!MasterManager.Instance.isInImaginary)
        {
            parentOfResponses = GameObject.FindGameObjectWithTag("OrderList").transform;
        }

        GameObject[] go = GameObject.FindGameObjectsWithTag("ObjCombi");
        numberOfCombinaison = go.Length / 2;
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
            isResolve = true;
            Setup();
            parentOfResponses.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }

    public void DisplayOrderList(string s)
    {
        var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);

        responceButton.GetComponentInChildren<TMP_Text>().text = s;
    }
}
