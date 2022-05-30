using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCollisionDetection : OnTriggerEvents
{
    public int maxDetectionAllowed;
    public int setCount;
    public int count;

    private void Start()
    {
        GetComponent<Collider>().enabled = setCollision;
    }
    public void IncreaseCount(int i) { count += i; }
    public int GetCount() { return count; }

    #region Trigger 3D Events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(other.name);
            IncreaseCount(setCount);
            triggerEnter?.Invoke();
        }
        else if (other.CompareTag(_tag) && count <= maxDetectionAllowed)
        {
            IncreaseCount(setCount);
            triggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(other.name);
            IncreaseCount(setCount);
            triggerExit?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(other.name);
            IncreaseCount(setCount);
            triggerStay?.Invoke();
        }
        else if (other.CompareTag(_tag) && count <= maxDetectionAllowed)
        {
            IncreaseCount(setCount);
            triggerStay?.Invoke();
        }
    }
    #endregion

    #region 2D Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(collision.name);
            IncreaseCount(setCount);
            triggerEnter2D?.Invoke();
        }
        else if (collision.CompareTag(_tag) && count <= maxDetectionAllowed)
        {
            IncreaseCount(setCount);
            triggerEnter2D?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(collision.name);
            IncreaseCount(setCount);
            triggerExit?.Invoke();
        }
        else if (collision.CompareTag(_tag) && count <= maxDetectionAllowed)
        {
            IncreaseCount(setCount);
            triggerExit?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag && debugEvent && count <= maxDetectionAllowed)
        {
            Debug.Log(collision.name);
            IncreaseCount(setCount);
            triggerStay?.Invoke();
        }
        else if (collision.CompareTag(_tag) && count <= maxDetectionAllowed)
        {
            IncreaseCount(setCount);
            triggerStay?.Invoke();
        }
    }
    #endregion
}
