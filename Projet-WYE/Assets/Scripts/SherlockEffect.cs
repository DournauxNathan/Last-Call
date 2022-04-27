using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlockEffect : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform calculatedTransform;
    public float distanceFromCamera;
    public float time;

    void FixedUpdate()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
        transform.rotation = cameraTransform.rotation;

        calculatedTransform = transform;
    }
}
