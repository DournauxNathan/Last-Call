using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float treshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    [Space(50)] public UnityEvent onPressed, onReleased;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
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
        var value = Vector3.Distance(startPos, transform.localPosition / joint.linearLimit.limit);

        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}