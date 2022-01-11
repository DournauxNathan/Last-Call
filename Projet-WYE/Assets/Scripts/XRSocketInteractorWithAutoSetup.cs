using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorWithAutoSetup : XRSocketInteractor
{

    [Header("")]
    [SerializeField]
    private bool isMergeable;
    [SerializeField]
    private GameObject mergewith;


    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.xRInteractionManager;
    }


    public override bool CanHover(XRBaseInteractable interactable)
    {
        if (isMergeable)
        {
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        return base.CanSelect(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (isMergeable)
        {
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        return base.CanSelect(interactable);
    }

    private bool MatchUsingGameObject(XRBaseInteractable interactable)
    {
        return interactable.gameObject == mergewith;
    }

}
