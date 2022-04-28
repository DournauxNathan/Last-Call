using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public bool isKinematic;
    private Rigidbody Rb;

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        Rb.isKinematic = isKinematic;
    }
}
