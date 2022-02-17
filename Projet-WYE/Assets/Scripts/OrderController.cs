using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class OrderController : Singleton<OrderController>
{
    public int currentNumberOfCombinaison;
    public int numberOfCombinaison;
    public List<OrderFormat> orders;
    public bool isResolve = false;

    [Header("New Section Code")]
    public GameObject a;
    public GameObject b;
    public bool switchObject;

    public List<Combinaison> combinaisons;
    public List<string> ordersStrings;

    private void Update()
    {        
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            AddCombinaison(a, b);
        }
    }

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
            SetResolve(true);
            SceneLoader.Instance.LoadNewScene("Office");            
        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }

    public void AddCombinaison(GameObject a, GameObject b)
    {
        Combinaison lastCombi = new Combinaison {
            currentCombinaison = a.name + "+ " + b.name,
            objetA = a.name,
            objetB = b.name,
        };

        combinaisons.Add(lastCombi);

        AddOrder(lastCombi.objetA, lastCombi.objetB);
    }

    public void AddOrder(string a, string b)
    {
       ordersStrings.Add("Use " + a + " with " + b + " to " + " make a thing");
    }

    public bool SetResolve(bool _bool) { return isResolve = _bool; }

    public bool GetResolve() { return isResolve; }
}

[System.Serializable]
public class Combinaison
{
    [HideInInspector] public string currentCombinaison;
    public string objetA;
    public string objetB;
}
