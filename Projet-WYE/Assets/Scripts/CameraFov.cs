using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;


public class CameraFov : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile volumeProfile;
    [SerializeField]
    UnityEvent changeFov;

    LensDistortion lens;

    [SerializeField]
    private bool testBool;

    private bool zoomIn = false;
    private bool zoomOut = false;

    private bool stateZoomBase = true;

    private float decay = 0f;


    // Start is called before the first frame update
    void Start()
    {
        if (changeFov == null)
        {
            changeFov = new UnityEvent();
        }

        LensSeeker();
        lens.intensity.value = decay;
        changeFov.AddListener(ZoomInOut);

        //ApplyEffcts();
    }

    // Update is called once per frame
    void Update()
    {
        if (testBool)
        {
            testBool = false;
            changeFov.Invoke();
        }

        if (decay>-1 && zoomOut)
        {
            decay -= (Time.deltaTime)/2;
            lens.intensity.value = decay;
        }
        else if (decay<=-1 && zoomOut)
        {
            zoomOut = false;
        }

        if (decay<0 && zoomIn)
        {
            decay += Time.deltaTime /2;
            lens.intensity.value = decay;
        }
        else if (decay>=0 && zoomIn)
        {
            zoomIn = false;
            lens.intensity.value = 0f;
            decay = 0f;
        }

    }

    public void ApplyEffcts(string effect)
    {
        var effectsComponents = volumeProfile.components;
        foreach (var e in effectsComponents)
        {
            if (e.name.Contains(effect) && e.active == false)
            {
                e.active = true;
            }
            else if (e.name.Contains(effect))
            {
                e.active = false;
            }
        }
    }

    public void FovZoomInOut()
    {
        

        if (stateZoomBase)
        {
            //lens.active = true;
            decay = 0f;
            zoomOut = true;
            stateZoomBase = false;
            Debug.Log("Starting Zoom Out");
        }
        else if (!stateZoomBase)
        {
            decay = -1f;
            zoomIn = true;
            stateZoomBase = true;
            Debug.Log("Starting Zoom In");
        }
    }


    private void LensSeeker()
    {
        var effectsComponents = volumeProfile.components;

        if (lens == null)
        {
            foreach (var e in effectsComponents)
            {
                if (e.name.Contains("LensDistortion"))
                {
                    lens = (LensDistortion)e;
                }
            }
        }
    }


    void ZoomInOut()
    {
        //ApplyEffcts(name);
    }
}
