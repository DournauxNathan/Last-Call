using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRGrabInteractableWithAutoSetup : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;

    public bool addOffset = false;

    protected override void Awake()
    {
        base.Awake();

        if (MasterManager.Instance != null)
        {
            interactionManager = MasterManager.Instance.references.xRInteractionManager;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //Create attach point
        if (!attachTransform && addOffset)
        {
            GameObject grab = new GameObject("Grap Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }
        else if (addOffset)
        {
            initialAttachLocalPos = attachTransform.localPosition;
            initialAttachLocalRot = attachTransform.transform.localRotation;
        }
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