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

    public int puzzlesSucced;
    public int puzzleNumber;

    //public List<OrderFormat> orders;
    public bool isResolve = false;

    public List<Combinaison> combinaisons;
    public List<Order> ordersStrings;

    [Header("Dev Menu Text")]
    public TMP_Text text;

    public List<string[]> outcomes;

    public void LateUpdate()
    {
        if (GetResolve())
        {
            Resolve();
        }
    }

    public int IncreaseValue(int _value)
    {        
        currentNumberOfCombinaison += _value;        
        Resolve();
        return currentNumberOfCombinaison;
    }

    public void ResolvePuzzle() { puzzlesSucced += 1; }
    public int GetNumberOfPuzzleSucced() { return puzzlesSucced; }


    public void Resolve()
    {
        if (currentNumberOfCombinaison == numberOfCombinaison || GetResolve())
        {
            MasterManager.Instance.isInImaginary = false;
            SetResolve(true);
            MasterManager.Instance.currentPhase = Phases.Phase_3;

        }
        else
        {
            //Debug.Log("All combinaison were not found");
        }
    }

    public void AddCombinaison(CombinableObject a, CombinableObject b, int _value, string _outcome, bool _lethality)
    {
        Combinaison newCombi = new Combinaison {
            currentCombinaison = a.name+ "+ " + b.name,
            objetA = a.name,
            objetB = b.name,
            value = _value,
            isLethal = _lethality
        }; 

        text.text = newCombi.currentCombinaison;

        combinaisons.Add(newCombi);

        PlaytestData.Instance.betaTesteurs.data.combinaisonsMade.Add(newCombi);

        AddOrder(newCombi.value, _outcome, newCombi.isLethal);
    }

    public void AddOrder(int value, string outcome, bool lethality)
    {
        Order newOrder = new Order {
            order = outcome,
            influence = value,
            isLethal = lethality
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
    public bool isLethal;
}

[System.Serializable]
public class Order
{
    public string order;
    public int influence;
    public bool isLethal;
    public AudioClip[] voiceLines;
}
