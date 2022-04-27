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
            useComparTag = false;
        }
    }
}
