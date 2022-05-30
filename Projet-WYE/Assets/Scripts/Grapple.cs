using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grapple : MonoBehaviour
{
    public Animator trapdoor;

    private bool hasEnter;
    private bool hasExit;

    public bool isComplete;
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

            Complete();
        }
    }

    public void Complete()
    {
        doAction?.Invoke();
        trapdoor.SetBool("Open", true);

        OrderController.Instance.AddOrder(1, outcome, false);
    }
}
