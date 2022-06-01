using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : Singleton<Teleport>
{
    private Transform position;
    public bool teleportAtStart;
    [SerializeField] private bool isActive;

    [SerializeField] private CapsuleCollider m_Collider;
    [SerializeField] private GameObject particle;

    public bool callEventAtStart;
    public UnityEvent doAction;

    private void Start()
    {
        if (MasterManager.Instance != null)
        {
            position = this.transform;

            if (GetComponentInChildren<Renderer>() != null)
            {
                GetComponentInChildren<Renderer>().enabled = isActive;
            }

            if (teleportAtStart && callEventAtStart)
            {
                doAction?.Invoke();

                MasterManager.Instance.references.player.transform.position = position.position;
                if (particle != null)
                {
                    particle.SetActive(false);
                }
            }
            else if (teleportAtStart)
            {
                MasterManager.Instance.references.player.transform.position = position.position;
                if (particle != null)
                {
                    particle.SetActive(false);
                }
            }   
        }
    }

    private void Update()
    {
        if (MasterManager.Instance != null && MasterManager.Instance.references.player.transform.position != position.position && MasterManager.Instance.references.player.transform != null)
        {
            m_Collider.isTrigger = false;
            
            if (GetComponentInChildren<Renderer>() != null)
            {
                GetComponentInChildren<Renderer>().enabled = true;
                if (particle != null)
                {
                    //particle.SetActive(true);
                }
            }
        }
    }

    public void TeleportTo()
    {
        MasterManager.Instance.references.player.GetComponent<VignetteApplier>().FadeIn();
        MasterManager.Instance.references.player.transform.position = position.position;

        doAction?.Invoke();

        m_Collider.isTrigger = true;
        particle.SetActive(false);

        if (GetComponentInChildren<Renderer>() != null)
        {
            GetComponentInChildren<Renderer>().enabled = false;
            if (particle != null)
            {
                particle.SetActive(false);
            }
        }
    }
}
