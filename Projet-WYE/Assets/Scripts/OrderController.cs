using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderController : MonoBehaviour
{
    public static OrderController instance;

    public int currentNumberOfCombinaison;
    public int numberOfCombinaison;
    public List<string> orders;

    public bool isResolve = false;

    public Transform parentOfResponses;
    public GameObject prefab_btnResponse;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        //parentOfResponses.gameObject.SetActive(false);
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
            Teleport.Instance.ReturnToOffice();
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
