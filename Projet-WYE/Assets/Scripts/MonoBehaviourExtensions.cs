using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public static class MonoBehaviourExtensions 
{
    public static void CallEvent(this MonoBehaviour mono, UnityEvent _event)
    {
        _event?.Invoke();
    }

    public static void CallWithDelay(this MonoBehaviour mono, Action method, float delay)
    {
        mono.StartCoroutine(CallWithDelayRoutine(method, delay));
    }


    static IEnumerator CallWithDelayRoutine(Action method, float delay)
    {
        yield return new WaitForSeconds(delay);
        method();
    }
}
