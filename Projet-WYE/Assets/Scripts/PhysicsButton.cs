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

    //public Mode currentMode;
        
    [Tooltip("Type of unit we want to send")]
    public Unit unitToSend;

    public GameObject clicker;
    public Material unlockColor;

    private float startYPosition;
    public float pressPosition;

    public bool isActivate = false;

    public UnityEvent onPressed;

    private Vector3 startPos;
    private SpringJoint springJoint;

    // Start is called before the first frame update
    void Start()
    {
        springJoint = GetComponentInChildren<SpringJoint>();
        springJoint.gameObject.GetComponent<BoxCollider>().enabled = isActivate;
/*
        if (currentMode == Mode.Unit)
        {
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= pressPosition)
        {
            OnPressed();
        }


        if (isActivate)
        {
            springJoint.gameObject.GetComponent<BoxCollider>().enabled = isActivate;
            clicker.GetComponent<Renderer>().material = unlockColor;
        }
    }

    public void OnPressed()
    {
        Debug.Log("Pressed !");
        onPressed?.Invoke();
    }

    public void SendUnit()
    {
        Debug.Log(unitToSend + " sent");
    }
}