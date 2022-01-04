using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorWithAutoSetup : XRSocketInteractor
{
    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.xRInteractionManager;
    }
}
