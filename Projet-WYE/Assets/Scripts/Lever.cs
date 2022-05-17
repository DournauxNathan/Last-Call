using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    public UnityEvent leverPulled;
    private Rigidbody rb;
    [Tooltip("min detection on z axis in rigid body, is trigger when z>minDetection")]public float minDetection;
    private bool hasTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //leverPulled.Invoke();

        if (rb.worldCenterOfMass.z > minDetection && !hasTrigger)
        {
            hasTrigger = true;
            leverPulled.Invoke();
        }
        else if (rb.worldCenterOfMass.z< minDetection)
        {
            hasTrigger = false;
        }
    }

    public void Pulled()
    {
        OrderController.Instance.ResolvePuzzle();
    }

}
