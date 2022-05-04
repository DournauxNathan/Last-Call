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
        limit = new OffsetLimit(-0.65f, 0.65f, -0.25f, 0.25f); //TODO: Change when testing in VR
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
        offsets.Add(new Vector2(_transform.localPosition.x, _transform.localPosition.y));
    }

    //Check if the offset is in the limit
    public void CheckOffset(Transform _transform){
        if(_transform.position.x> limit.maxX|| _transform.position.x< limit.minX|| _transform.position.y> limit.maxY|| _transform.position.y< limit.minY){
            int index = _transform.GetSiblingIndex();
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

}