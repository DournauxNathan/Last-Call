using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(Outline), typeof(SphereCollider))]
public class CombinableObject : CombinableObject_Data
{
    public bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        SetOutline();

        //ToggleInteractor(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjCombi"))
        {
            ListManager.Instance.CheckCompatibility(this.gameObject, other.gameObject);
            Debug.Log(this.gameObject.name + " | " + other.name);
        }

        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Disable hand colliDer");
            //other.GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.tag + "/n" + "Enable hand colliDer");
            other.GetComponent<MeshCollider>().enabled = true;
        }
    }

    public void ToggleOutline(bool b)
    {
        if (!isLocked)
        {
            outline.enabled = b;
        }
        else
        {
            outline.enabled = true;
        }
    }

    public void Lock(bool b)
    {
        if (MasterManager.Instance.isInImaginary && b)
        {
            isLocked = true;
            onLock?.Invoke();

            outline.OutlineColor = selectOutline.color;
        }
        else if (MasterManager.Instance.isInImaginary && !b)
        {
            isLocked = false;
            onUnlock?.Invoke();

            outline.OutlineColor = defaultOutlineColor;

            ToggleOutline(b);
        }
    }

    public void ToggleInteractor(bool value)
    {
        if (TryGetComponent<XRGrabInteractableWithAutoSetup>(out XRGrabInteractableWithAutoSetup XrGrabComponent))
        {
            XrGrabComponent.enabled = value;
        }
        else if (TryGetComponent<XRSimpleInteractableWithAutoSetup>(out XRSimpleInteractableWithAutoSetup XrSimpleComponent))
        {
            XrSimpleComponent.enabled = value;
        }
    }
}