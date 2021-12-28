using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRGrabInteractableWithAutoSetup : XRGrabInteractable
{
    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.xRInteractionManager;
    }

}