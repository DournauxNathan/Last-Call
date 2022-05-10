using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHitInfoRayInteractor : XRRayInteractor
{
    public ListManager interactors;
    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.references.xRInteractionManager;
    }

    public void GetHoverInteractors()
    {
        if (interactors.hoveredInteractors.Count == 0)
        {
            interactors.hoveredInteractors.Add(validTargets[0].gameObject);
        }

        if (!interactors.hoveredInteractors.Contains(validTargets[0].gameObject))
        {
            interactors.hoveredInteractors.Add(validTargets[0].gameObject);
        }
        else
        {
            //Debug.LogWarning("There already this object in the interactors list");
        }
    }

}
