using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fracture : MonoBehaviour
{
    public string outcome;
    public int maxDetection;
    public int count;

    public UnityEvent noOutcome,doAction;

    public bool sendOutcome;


    public bool isComplete, wasComplete;
    public bool doCheck = true;
    
    public void Check()
    {
        if (count < maxDetection && !isComplete)
        {
            count++;

            if (count == maxDetection)
            {
                noOutcome?.Invoke();

                if (doCheck)
                {
                    isComplete = true;
                    SendOutcome();
                }
            }
        }
    }

    public void SendOutcome()
    {
        if (isComplete && !wasComplete)
        {
            isComplete = false;
            wasComplete = true;

            doAction?.Invoke();
            OrderController.Instance.AddOrder(1, outcome, false);
        }

    }
}
