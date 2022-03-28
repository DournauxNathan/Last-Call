using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlockEffect : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Transform cameraTransform;
    public float time;
    //Set it to whatever value you think is best
    public float distanceFromCamera;

    public Transform calculatedTransform;


    void Start()
    {
        cameraTransform = MasterManager.Instance.xrRig.transform.GetChild(0).GetChild(0);
    }


    void Update()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
        transform.rotation = cameraTransform.rotation;

        calculatedTransform = transform;
    }
}
