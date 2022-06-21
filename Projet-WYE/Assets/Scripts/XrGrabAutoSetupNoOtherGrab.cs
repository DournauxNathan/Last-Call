using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class XrGrabAutoSetupNoOtherGrab : XRGrabInteractable
{
    [SerializeField] private bool isGrabbed = false;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if (MasterManager.Instance != null)
        {
            interactionManager = MasterManager.Instance.references.xRInteractionManager;
        }
    }

    protected override void Grab()
    {
        if (!isGrabbed)
        {
            isGrabbed = true;
            base.Grab();
        }
    }

    protected override void Detach()
    {
        base.Detach();
        isGrabbed = false;
    }
}
