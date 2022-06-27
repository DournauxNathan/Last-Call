using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorWithAutoSetup : XRSocketInteractor
{

    [Header("Put Together Mechanics")]
    [SerializeField]
    private bool canAssemble;
    public GameObject snapToA;
    public GameObject snapToB;

    [SerializeField] private bool tagCombi;
    [SerializeField] private string tagAssemble;

    public UnityEvent action;

    public bool isMatching;

    protected override void Awake()
    {
        base.Awake();
        interactionManager = MasterManager.Instance.references.xRInteractionManager;
    }

    protected override void Start()
    {
        base.Start();
    }

    public bool doOnce = true;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        if (canAssemble)
        {
            if (doOnce)
            {
                doOnce = false;

                action?.Invoke();
            }
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        else if (tagCombi)
        {
            if (doOnce)
            {
                doOnce = false;

                action?.Invoke();
            }
            return base.CanSelect(interactable) && MatchUsingTags(interactable);
        }

        return base.CanSelect(null);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        if (canAssemble)
        {
            if (doOnce)
            {
                doOnce = false;

                action?.Invoke();
            }
            return base.CanSelect(interactable) && MatchUsingGameObject(interactable);
        }
        else if (tagCombi)
        {
            action?.Invoke();
            return base.CanSelect(interactable) && MatchUsingTags(interactable);
        }

        return base.CanSelect(null);
    }

    public bool MatchUsingGameObject(XRBaseInteractable interactable)
    {
        isMaching(interactable.gameObject == snapToA || interactable.gameObject == snapToB);
        return interactable.gameObject == snapToA || interactable.gameObject == snapToB;
    }

    private bool MatchUsingTags(XRBaseInteractable interactable)
    {
        isMaching(interactable.gameObject.tag == tagAssemble);
        return interactable.gameObject.tag == tagAssemble;
    }

    public bool isMaching(bool b)
    {
        return isMatching = b;
    }

    public void SetSocket()
    {

    }

}
