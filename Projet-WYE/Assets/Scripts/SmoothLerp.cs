using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLerp : MonoBehaviour
{
    public Transform target;
    public Transform lookAt;

    public float speed = 2.0f;

    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, target.position.y, interpolation);
        position.x = Mathf.Lerp(transform.position.x, target.position.x, interpolation);

        transform.position = position;


        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, interpolation);
    }
}
