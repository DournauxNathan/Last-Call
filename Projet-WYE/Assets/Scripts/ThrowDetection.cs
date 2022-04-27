using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDetection : MonoBehaviour
{
    public string _tag;
    public bool useComparTag;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.CompareTag(_tag) && useComparTag && other.GetComponent<CombinableObject>() != null);

        if (other.CompareTag(_tag) && useComparTag && other.GetComponent<CombinableObject>() != null)
        {
            if (OrderController.Instance.GetNumberOfPuzzleSucced() < 0 && OrderController.Instance.GetNumberOfPuzzleSucced() > 1)
            {
                OrderController.Instance.ResolvePuzzle();
            }
        }
    }
}
