using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDetection : MonoBehaviour
{
    public string _tag;
    public bool useComparTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && other.GetComponent<CombinableObject>() != null)
        {
            OrderController.Instance.ResolvePuzzle();

            other.TryGetComponent<CombinableObject>(out CombinableObject _combinableObject);
            OrderController.Instance.AddOrder(_combinableObject.useWith[0].influence, _combinableObject.useWith[0].outcome, _combinableObject.useWith[0].isLethal);

            useComparTag = false;
        }
        else
        {
            Debug.Log("A new object has been thrown");
        }
    }
}
