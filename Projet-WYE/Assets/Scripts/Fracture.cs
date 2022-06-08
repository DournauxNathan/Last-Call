using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fracture : MonoBehaviour
{
    public string outcome;
    public int maxDetection;
    public int count;

    public UnityEvent doAction;

    public bool sendOutcome;

    public void Check()
    {
        if (count < maxDetection)
        {
            count++;

            if (count == maxDetection)
            {
                SendOutcome();
            }
        }
    }

    public void SendOutcome()
    {
        doAction?.Invoke();
        OrderController.Instance.AddOrder(1, outcome, false);
    }
}
