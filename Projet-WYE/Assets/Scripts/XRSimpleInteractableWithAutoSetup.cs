using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;

public class XRSimpleInteractableWithAutoSetup : XRSimpleInteractable
{
    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.references.xRInteractionManager;
        
        if (GetComponent<MeshCollider>() != null)
        {
            colliders[0] = GetComponent<MeshCollider>();
        }

        //Debug.Log(gameObject.name + " " + hoverEntered.GetPersistentEventCount());
    }
}
