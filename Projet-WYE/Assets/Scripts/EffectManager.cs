using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;


public class EffectManager : MonoBehaviour
{/*
    [SerializeField] private CameraFov cameraFov;
    [SerializeField] private Volume volume;
    [SerializeField] private UnityEvent unityEvent;
    void Start()
    {
        SetUp();
        //unityEvent.Invoke();
    }
    void Update()
    {
        
    }

    public void SetUp()
    {
        volume = FindObjectOfType<Volume>();
        if (volume != null)
        {
            cameraFov = GetComponent<CameraFov>();
            unityEvent = cameraFov.changeFov;
        }
        else
        {
            Debug.LogError("PostProces not found");
        }
    }

    public void TriggerZoom()
    {
        unityEvent.Invoke();
    }*/
}