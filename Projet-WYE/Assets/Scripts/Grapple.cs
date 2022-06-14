using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grapple : MonoBehaviour
{
    public Animator trapdoor;

    private bool hasEnter;
    private bool hasExit;

    public bool isComplete, wasComplete;
    public UnityEvent doAction;

    public string outcome;

    public void Enter()
    {
        hasEnter = true;
    }

    public void Exit()
    {
        if (hasEnter)
        {
            hasEnter = false;
            hasExit = true;

            if (!isComplete)
            {
                Complete();
            }
        }
    }

    public void Complete()
    {
        isComplete = true;

        if (isComplete && !wasComplete)
        {
            wasComplete = true;
            doAction?.Invoke();
            trapdoor.SetBool("Open", true);

            OrderController.Instance.AddOrder(1, outcome, false);

            OrderController.Instance.ResolvePuzzle();
        }
    }
}
