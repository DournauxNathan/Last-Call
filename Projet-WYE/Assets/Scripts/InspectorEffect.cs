using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorEffect : Singleton<InspectorEffect>
{
    [SerializeField]private Transform cameraTransform;
    [Header("parameters")]
    public Transform calculatedTransform;
    public Transform objectTransform;
    public float distanceFromObject;
    public float time; // 0f is not moving


    void Start()
    {
        cameraTransform = MasterManager.Instance.references.mainCamera; //null if PersistentC is not Active scene
        calculatedTransform = transform;
    }

    void FixedUpdate()
    {
        if(objectTransform != null)
        {
            Vector3 resultingPosition = objectTransform.position + objectTransform.forward * distanceFromObject;

            transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time); //track the object
            transform.rotation = cameraTransform.rotation; //Keep the UI in the same rotation as the camera
        }
    }
}
