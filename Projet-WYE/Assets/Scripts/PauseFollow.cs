using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFollow : MonoBehaviour
{
    public Transform cameraTransform;
    public float time;
    //Set it to whatever value you think is best
    public float distanceFromCamera;

    private void Start()
    {
        if (MasterManager.Instance != null)
        {
            cameraTransform = MasterManager.Instance.references.mainCamera;

        }
        else
        {
            Debug.LogError("Error no camera to follow");
        }
    }

    void Update()
    {
        Vector3 resultingPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = Vector3.Lerp(transform.position,resultingPosition,Time.deltaTime * time);
        transform.rotation = cameraTransform.rotation;
    }
}
