using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public enum Mode
    {
        Unit,
        Physic,
    }

    public Mode currentMode;

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

    // Start is called before the first frame update
    void Start()
    {
        startPosition = push.transform.localPosition;
        /*
        if (currentMode == Mode.Unit)
        {
        }*/
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {            
            Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }

        if (isActivate)
        {
            clicker.GetComponent<Renderer>().material = unlockColor;
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
        //Debug.Log("Pressed");

    }

    public void Released()
    {
        isPressed = false;
        onReleased?.Invoke();
        //Debug.Log("Released");
    }

    public void SendUnit()
    {
        Debug.Log(unitToSend + " sent");
    }
}