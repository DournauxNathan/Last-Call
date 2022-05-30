using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnTriggerEvents : MonoBehaviour
{
    public string _tag;
    public bool useComparTag;
    public bool debugEvent = false;

    public bool setCollision = true;

    [Header("Trigger Events")]
    public bool debugEnter;
    public UnityEvent triggerEnter;
    public bool debugExit;
    public UnityEvent triggerExit;
    public bool debugStay;
    public UnityEvent triggerStay;

    [Header("2D Trigger Events")]
    public UnityEvent triggerEnter2D;
    public bool debugEnter2D;
    public UnityEvent triggerExit2D;
    public bool debugExit2D;
    public UnityEvent triggerStay2D;
    public bool debugStay2D;

    private void Start()
    {
        GetComponent<Collider>().enabled = setCollision;
    }

    private void FixedUpdate()
    {
        if (debugEnter)
        {
            debugEnter = !debugEnter;
            this.CallEvent(triggerEnter);
        }
        if (debugExit)
        {
            debugExit = !debugExit;
            this.CallEvent(triggerExit);
        }
        if (debugStay)
        {
            debugStay = !debugStay;
            this.CallEvent(triggerStay);
        }

        if (debugEnter2D)
        {
            debugEnter2D = !debugEnter2D;
            this.CallEvent(triggerEnter2D);
        }
        if (debugExit2D)
        {
            debugExit2D = !debugExit2D;
            this.CallEvent(triggerExit2D);
        }
        if (debugStay2D)
        {
            debugStay2D = !debugStay2D;
            this.CallEvent(triggerStay2D);
        }
    }

    #region Trigger 3D Events
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag && debugEvent)
        {
            Debug.Log(other.name);
            triggerEnter?.Invoke();
        }
        else if (other.CompareTag(_tag) && useComparTag)
        {
            triggerEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag) && useComparTag)
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
