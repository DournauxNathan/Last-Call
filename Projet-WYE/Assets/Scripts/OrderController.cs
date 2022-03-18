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
    //public List<OrderFormat> orders;
    public bool isResolve = false;

    public List<Combinaison> combinaisons;
    public List<Order> ordersStrings;

    [Header("Dev Menu Text")]
    public TMP_Text text;

    public List<string[]> outcomes;

    public void Setup()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("ObjCombi");

        ObjectActivator.Instance.SetActivetObject(go);
        //numberOfCombinaison = ObjectActivator.Instance.indexesList.Count / 2;
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
            
            Projection.Instance.isTransition = true;
            Projection.Instance.hasProjted = true;
            Projection.Instance.Deconstruct();
        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }

    public void AddCombinaison(CombinableObject a, CombinableObject b, int _value, string _outcome)
    {
        Combinaison newCombi = new Combinaison {
            currentCombinaison = a.name+ "+ " + b.name,
            objetA = a.name,
            objetB = b.name,
            value = _value,
        }; 

        text.text = newCombi.currentCombinaison;

        combinaisons.Add(newCombi);

        PlaytestData.Instance.betaTesteurs.data.combinaisonsMade.Add(newCombi);

        AddOrder(newCombi.value, _outcome);
    }

    public void AddOrder(int value, string outcome)
    {
        Order newOrder = new Order {
            order = outcome,
            influence = value,
        };

        ordersStrings.Add(newOrder);
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
    public int value;
}

[System.Serializable]
public class Order
{
    public string order;
    public int influence;
    public AudioClip[] voiceLines;
}
