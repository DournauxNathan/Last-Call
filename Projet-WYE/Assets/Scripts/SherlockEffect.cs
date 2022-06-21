using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlockEffect : Singleton<SherlockEffect>
{
    public Transform cameraTransform;
    public Transform calculatedTransform;
    public float distanceFromCamera;
    public float time;
    public float XLimit;
    public float lerpPosForImaginary = 0.8f;

    private CameraRoatationLimits cameraLimit;

    public bool invertLimitDebug = false;

    private void Start() {
        cameraTransform = MasterManager.Instance.references.mainCamera;
        cameraLimit = new CameraRoatationLimits(XLimit);
        if(cameraTransform == null)cameraTransform = MasterManager.Instance.references.mainCamera;
    }

    void FixedUpdate()
    {
        //Have no reference
        
        if(cameraLimit.xLimit != XLimit) //TODO: Remove Only to find the corect value
        {
            cameraLimit.xLimit = XLimit;
        }
/*
        if (MasterManager.Instance.currentPhase == Phases.Phase_2)
        {
            distanceFromCamera = 0.55f;
            XLimit = 0.075f;
        }
        else
        {
            XLimit = 0;
        }*/

        
        if(MasterManager.Instance.currentPhase != Phases.Phase_2){
            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
            transform.rotation = cameraTransform.rotation;

            calculatedTransform = transform;

        }
        else if(cameraLimit.CheckCameraXLimit(cameraTransform) && MasterManager.Instance.currentPhase == Phases.Phase_2)
        {
            //block the movement of the UI
            transform.rotation = cameraTransform.rotation; //Keep the UI in the same rotation as the camera
            Vector3 lerpPosition = new Vector3(cameraTransform.position.x, lerpPosForImaginary, cameraTransform.position.z);
            transform.position = Vector3.Lerp(transform.position, lerpPosition, Time.deltaTime * time);
        }
        else if(!cameraLimit.CheckCameraXLimit(cameraTransform) && MasterManager.Instance.currentPhase == Phases.Phase_2) // work only if the camera is not in the limit
        {
            Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
            transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
            transform.rotation = cameraTransform.rotation;

            calculatedTransform = transform;
        }
    }
}

public class CameraRoatationLimits : SherlockEffect
{
    public float xLimit;
    
    public CameraRoatationLimits(float xLimit) //Constructor
    {
        this.xLimit = xLimit;
    }

    public bool CheckCameraXLimit(Transform _transform) //Check if the camera above the limit
    {
        if(_transform.localRotation.x < xLimit && !invertLimitDebug)
        {
            //Debug.Log("Camera looking up");
            return true;
        }
        else if(_transform.localRotation.x > xLimit && invertLimitDebug)
        {
            //Debug.Log("Camera looking up");
            return true;
        }
        else
        {
            return false;
        }
    }
}