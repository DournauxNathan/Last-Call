using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [Header("")]
    [Tooltip("Type of unit we want to send")]
    public Unit unitToSend;
    public bool isActivate = false;

    [Header("Physics properties")]
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.25f;
    [Space(5)]
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private bool isPressed;
    private Vector3 startPosition;

    [Header("Refs")]
    public GameObject push;
    public GameObject clicker;
    public ConfigurableJoint joint;
    public Material unlockColor;
    public Material lockColor;

    public int nPress = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = push.transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isPressed && GetValue() + threshold >= 1 && isActivate)
        {            
            Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0 && isActivate)
        {
            Released();
        }

        if (isActivate)
        {
            push.GetComponent<BoxCollider>().enabled = true;
            clicker.GetComponent<Renderer>().material = unlockColor;
        }
        else
        {
            push.GetComponent<BoxCollider>().enabled = false;
            clicker.GetComponent<Renderer>().material = lockColor;
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPosition, push.transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    public void Pressed()
    {
        isPressed = true;
        onPressed?.Invoke();
    }

    public void IncreaseNumberOfPress()
    {
        nPress++;

        if (nPress == 1)
        {
            UnitDispatcher.Instance.UpdateUI();
        }
        else if (nPress == 2)
        {
            UnitDispatcher.Instance.NextSequence();
            UnitDispatcher.Instance.UpdateUI();
            nPress = 2;
        }
    }

    public void Released()
    {
        isPressed = false;
        onReleased?.Invoke();
    }

    public void SendUnit()
    {
        if (!UnitDispatcher.Instance.unitsSend.Contains(unitToSend))
        {
            UnitDispatcher.Instance.unitsSend.Add(unitToSend);
        }   

        //SaveQuestion.Instance.savedDataFrom[0].unitSended.Add(unitToSend);
    }
}