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
    public bool debugEnter;
    public UnityEvent triggerExit;
    public bool debugExit;
    public UnityEvent triggerStay;
    public bool debugStay;

    [Header("2D Trigger Events")]
    public UnityEvent triggerEnter2D;
    public bool debugEnter2D;
    public UnityEvent triggerExit2D;
    public bool debugExit2D;
    public UnityEvent triggerStay2D;
    public bool debugStay2D;

    private void FixedUpdate()
    {
        if (debugEnter)
        {
            debugEnter = !debugEnter;
            triggerEnter?.Invoke();
        }
        if (debugExit)
        {
            debugExit = !debugExit;
            triggerExit?.Invoke();
        }
        if (debugStay)
        {
            debugStay = !debugStay;
            triggerStay?.Invoke();
        }

        if (debugEnter2D)
        {
            debugEnter2D = !debugEnter2D;
            triggerEnter2D?.Invoke();
        }
        if (debugExit2D)
        {
            debugExit2D = !debugExit2D;
            triggerExit2D?.Invoke();
        }
        if (debugStay2D)
        {
            debugStay2D = !debugStay2D;
            triggerStay2D?.Invoke();
        }
    }

    #region Trigger 3D Events
    private void OnTriggerEnter(Collider other)
    {
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
