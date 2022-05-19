using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLerp : MonoBehaviour
{
    public Transform target;
    public Transform lookAt;

    public float time;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.position, time);
        //transform.LookAt(target);
    }
}
