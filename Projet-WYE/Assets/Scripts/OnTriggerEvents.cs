using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnTriggerEvents : MonoBehaviour
{
    public string _tag;
    public bool useComparTag;

    [Header("Trigger Events")]
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;
    public UnityEvent triggerStay;

    [Header("2D Trigger Events")]
    public UnityEvent triggerEnter2D;
    public UnityEvent triggerExit2D;
    public UnityEvent triggerStay2D;

    #region Trigger 3D Events
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag(_tag) && useComparTag)
        {
            Debug.Log(other.name);
            triggerEnter?.Invoke();
        }
        else
        {
            Debug.Log(other.name);
            triggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag)
        {
            triggerExit?.Invoke();
        }
        else
        {
            triggerExit?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag)
        {
            triggerStay?.Invoke();
        }
        else
        {
            triggerStay?.Invoke();
        }
    }
    #endregion

    #region 2D Trigger Events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag)
        {
            triggerEnter2D?.Invoke();
        }
        else
        {
            triggerEnter2D?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag)
        {
            triggerExit?.Invoke();
        }
        else
        {
            triggerExit?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(_tag) && useComparTag)
        {
            triggerStay?.Invoke();
        }
        else
        {
            triggerStay?.Invoke();
        }
    }
    #endregion
}
