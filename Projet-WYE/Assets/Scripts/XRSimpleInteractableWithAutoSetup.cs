using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class XRSimpleInteractableWithAutoSetup : XRSimpleInteractable
{    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.xRInteractionManager;
    }
}
