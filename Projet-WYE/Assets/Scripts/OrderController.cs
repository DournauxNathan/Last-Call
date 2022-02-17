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

    [Header("New Section Code")]
    public CombinableObject a;
    public CombinableObject b;

    public List<Combinaison> combinaisons;
    public List<Order> ordersStrings;

    private void Update()
    {        
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            AddCombinaison(a, b);
            IncreaseValue(1);
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
            MasterManager.Instance.currentPhase = Phases.Phase_3;
            SceneLoader.Instance.LoadNewScene("Office");            
        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }

    public void AddCombinaison(CombinableObject a, CombinableObject b)
    {
        Combinaison newCombi = new Combinaison {
            currentCombinaison = a.gameObject.name + "+ " + b.gameObject.name,
            objetA = a.gameObject.name,
            objetB = b.gameObject.name,
            value = a.influence + b.influence,
        };

        combinaisons.Add(newCombi);

        AddOrder(newCombi.objetA, newCombi.objetB, newCombi.value);
    }

    public void AddOrder(string a, string b, int value)
    {
        Order newOrder = new Order {
            order = "Use " + a + " with " + b + " to " + " make a thing",
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
