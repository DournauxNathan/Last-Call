using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script to use with a physical button
/// </summary>
/// 


public class PhysicsButton : MonoBehaviour
{
    public enum Mode
    {
        Unit,
        Physic,
    }

    public Mode currentMode;

    public float treshold = 0.1f;
    public float deadZone = 0.025f;
    public Transform childObject;
    
    [Tooltip("Type of unit we want to send")]
    public Unit unitToSend;

    public GameObject clicker;
    public Material unlockColor;

    public bool isActivate = false;

    public UnityEvent onPressed, onReleased;

    private bool isPressed = true;
    private Vector3 startPos;
    private SpringJoint springJoint;


    // Start is called before the first frame update
    void Start()
    {
        startPos = childObject.localPosition;
        springJoint = GetComponentInChildren<SpringJoint>(); 

        if (currentMode == Mode.Unit)
        {
            springJoint.gameObject.GetComponent<BoxCollider>().enabled = isActivate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + treshold >= 1)
            Pressed();

        if (isPressed && GetValue() - treshold <= 0)
            Released();

        if (isActivate)
        {
            springJoint.gameObject.GetComponent<BoxCollider>().enabled = isActivate;
            clicker.GetComponent<Renderer>().material = unlockColor;
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, childObject.localPosition / springJoint.minDistance);

        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        if (isActivate)
        {
            isPressed = true;
            Debug.Log(isPressed);
            SendUnit(unitToSend);
            //onPressed.Invoke();
        }
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }

    public void SendUnit(Unit currentUnit)
    {
        //Debug.Log(currentUnit + " sent");
    }
}