using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrapInteractable : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    // Start is called before the first frame update
    void Start()
    {
        //Create attach point
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grap Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.transform.localRotation;
    }

    [System.Obsolete]
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos; 
            attachTransform.localRotation = initialAttachLocalRot;
        }

        base.OnSelectEntered(interactor);
    }
}
