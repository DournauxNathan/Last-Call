using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowDetection : MonoBehaviour
{
    public string _tag;
    public bool useComparTag;

    public UnityEvent doAction;

    public bool isComplete, wasComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && other.GetComponent<CombinableObject>() != null)
        {
            isComplete = true;

            if (isComplete && !wasComplete)
            {
                isComplete = true;
                wasComplete = true;

                //OrderController.Instance.ResolvePuzzle();

                other.TryGetComponent<CombinableObject>(out CombinableObject _combinableObject);
                OrderController.Instance.AddOrder(_combinableObject.useWith[0].influence, _combinableObject.useWith[0].outcome, _combinableObject.useWith[0].isLethal);

                _combinableObject.useWith[0].doAction?.Invoke();

                useComparTag = false;

                doAction?.Invoke();
            }
        }
        else
        {
            //Debug.Log(other.name + " has been thrown");
        }
    }
}
