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
    [SerializeField] private bool tagCombi;
    [SerializeField] private string tagAssemble;


    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.references.xRInteractionManager;
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
        else if (tagCombi)
        {
            return base.CanSelect(interactable) && MatchUsingTags(interactable);
        }
        return base.CanSelect(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (canAssemble)
        {
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        else if (tagCombi)
        {
            return base.CanSelect(interactable) && MatchUsingTags(interactable);
        }
        return base.CanSelect(interactable);
    }

    private bool MatchUsingGameObject(XRBaseInteractable interactable)
    {
        return interactable.gameObject == snapTo;
    }

    private bool MatchUsingTags(XRBaseInteractable interactable)
    {
        return interactable.gameObject.tag == tagAssemble;
    }

}
