using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hammer : MonoBehaviour
{
    public CombinableObject hammer;

    public int count;
    public int maxNailsToDrive;

    private bool isComplete, wasComplete;

    public UnityEvent doAction;

    private void Update()
    {
        
    }

    public void Increase()
    {
        count += 1;

        if (count >= maxNailsToDrive)
        {
            isComplete = true;
            Check();
        }
    }

    public void Check()
    {
        if (isComplete && !wasComplete)
        {
            isComplete = false;
            wasComplete = true;

            OrderController.Instance.AddOrder(hammer.useWith[0].influence, hammer.useWith[0].outcome, hammer.useWith[0].isLethal);
            //OrderController.Instance.ResolvePuzzle();

            doAction?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nail"))
        {
            //Debug.Log(other.name);
            other.GetComponent<Nails>().drive?.Invoke();
        }
    }
}
