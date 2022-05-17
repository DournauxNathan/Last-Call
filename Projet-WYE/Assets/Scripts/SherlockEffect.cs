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
    public List<Vector2> offsets;

    public OffsetLimit limit;
    private CameraRoatationLimits cameraLimit;

    public bool invertLimitDebug = false;

    private void Start() {
        limit = new OffsetLimit(-1f, 1f, 0.63f, 1.35f); //TODO: Change when testing in VR    // maxY must be >1.2f Y  /!\axis is offseted
        cameraLimit = new CameraRoatationLimits(XLimit);
    }
    void LateUpdate()
    {
        if(cameraLimit.xLimit != XLimit) //TODO: Remove Only to find the corect value
        {
            cameraLimit.xLimit = XLimit;
        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_2)
        {
            distanceFromCamera = 0.55f;
            XLimit = 0.075f;
        }
        else
        {
            XLimit = 0;
        }

        
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

    //Check if the offset is in the limit
    public void CheckOffset(Transform _transform){

        bool isInLimitMaxX = _transform.position.x> limit.maxX ;
        bool isInLimitMinX = _transform.position.x < limit.minX ;
        bool isInLimitMaxY = _transform.position.y > limit.maxY ;
        bool isInLimitMinY = _transform.position.y < limit.minY ;

        if(isInLimitMaxX || isInLimitMinX || isInLimitMaxY || isInLimitMinY)
        {
            int index = _transform.GetSiblingIndex(); 
            Debug.Log(index +"\n"+ _transform.gameObject.name+"\n"+"minX: "+isInLimitMinX+" maxX: "+isInLimitMaxX+" minY: "+isInLimitMinY+" MaxY: "+isInLimitMaxY); //TODO: Remove
            Debug.Log( "x: "+ _transform.position.x+" y: "+_transform.position.y);                                                                                  //TODO: Remove
            _transform.position = new Vector3(offsets[index].x, offsets[index].y, _transform.position.z);
        }

    }
}

//class OffsetLimit{
public class OffsetLimit
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public OffsetLimit (float minX, float maxX, float minY, float maxY){ //Constructor
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }
    public override string ToString(){
        return "minX: "+minX+" maxX: "+maxX+" minY: "+minY+" MaxY: "+maxY;
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