using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SherlockEffect : Singleton<SherlockEffect>
{
    public Transform cameraTransform;
    public Transform calculatedTransform;
    public float distanceFromCamera;
    public float time;
    public List<Vector2> offsets;

    public OffsetLimit limit;

    private void Start() {
        limit = new OffsetLimit(-1f, 1f, 0.63f, 1.35f); //TODO: Change when testing in VR    // maxY must be >1.2f Y  /!\axis is offseted
        Debug.Log(limit.ToString()); //TODO: Remove
    }


    void FixedUpdate()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = Vector3.Lerp(transform.position, resultingPosition, Time.deltaTime * time);
        transform.rotation = cameraTransform.rotation;

        calculatedTransform = transform;
    }

    public void AddOffset(Transform _transform)
    {
        offsets.Add(new Vector2(_transform.position.x, _transform.position.y));
    }

    //Check if the offset is in the limit
    public void CheckOffset(Transform _transform){

        var isInLimitMaxX = _transform.position.x> limit.maxX ;
        var isInLimitMinX = _transform.position.x < limit.minX ;
        var isInLimitMaxY = _transform.position.y > limit.maxY ;
        var isInLimitMinY = _transform.position.y < limit.minY ;

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