using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorWithAutoSetup : XRSocketInteractor
{

    [Header("Put Together Mechanics")]
    [SerializeField]
    private bool canAssemble;
    [SerializeField]
    private GameObject snapTo;


    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.xRInteractionManager;
    }

    protected override void Start()
    {
        base.Start();
    }


    public override bool CanHover(XRBaseInteractable interactable)
    {
        if (canAssemble)
        {
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        return base.CanSelect(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (canAssemble)
        {
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        return base.CanSelect(interactable);
    }

    private bool MatchUsingGameObject(XRBaseInteractable interactable)
    {
        return interactable.gameObject == snapTo;
    }

}
