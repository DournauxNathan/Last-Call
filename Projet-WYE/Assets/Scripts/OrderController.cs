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

            if (orders.Count !=0)
            {
                for (int i = 0; i < orders.Count; i++)
                {
                    DisplayOrderList(orders[i]);
                }
            }
        }


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

    public void DisplayOrderList(string s)
    {
        var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);

        responceButton.GetComponentInChildren<TMP_Text>().text = s;
    }
}
