using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 Script to use with a physical button
 */

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float treshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    public Transform childObject;
    
    public Unit unitToSend;

    private bool isPressed = true;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    [Space(50)] public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = childObject.localPosition;
        joint = GetComponentInChildren<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + treshold >= 1)
            Pressed();

        if (isPressed && GetValue() - treshold <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, childObject.localPosition / joint.linearLimit.limit);

        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        SendUnit(unitToSend);
        //onPressed.Invoke();
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        //Debug.Log("Released");
    }

    public void SendUnit(Unit currentUnit)
    {
        Debug.Log(currentUnit + " sent");
    }
}