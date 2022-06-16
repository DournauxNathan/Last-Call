using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

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
    public static void CallTypeWriter(this MonoBehaviour mono, TMP_Text _text, string s)
    {
        mono.StartCoroutine(TypeWriterTMP(_text, s));
    }

    static IEnumerator TypeWriterTMP(TMP_Text _tmpProText, string _writer)
    {
        _tmpProText.text += MonoExtensionData.leadingCharBeforeDelay ? MonoExtensionData.leadingChar : " ";

        yield return new WaitForSeconds(MonoExtensionData.delayBeforeStart);

        foreach (char c in _writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - MonoExtensionData.leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += MonoExtensionData.leadingChar;
            yield return new WaitForSeconds(MonoExtensionData.timeBtwChars);
        }

        if (MonoExtensionData.leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - MonoExtensionData.leadingChar.Length);
        }
    }
}

public class MonoExtensionData : MonoBehaviour
{

    public static float delayBeforeStart = 0f;
    [HideInInspector] public static float timeBtwChars = 0.1f;
    [HideInInspector] public static string leadingChar = "";
    [HideInInspector] public static bool leadingCharBeforeDelay = false;
}
