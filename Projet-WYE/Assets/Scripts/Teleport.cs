using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    private Transform position;
    public bool teleportAtStart;
    [SerializeField] private bool isActive;

    [SerializeField] private CapsuleCollider m_Collider;

    private void Start()
    {
        position = this.transform;
        
        if (GetComponentInChildren<Renderer>() != null)
        {
            GetComponentInChildren<Renderer>().enabled = isActive;
        }
        
        if (teleportAtStart)
        {
            MasterManager.Instance.references.player.transform.position = position.position;
        }
    }
    private void Update()
    {
        if (MasterManager.Instance.references.player.transform.position != position.position && MasterManager.Instance.references.player.transform != null)
        {
            m_Collider.isTrigger = false;
            
            if (GetComponentInChildren<Renderer>() != null)
            {
                GetComponentInChildren<Renderer>().enabled = true;
            }
        }
    }

    public void TeleportTo()
    {
        MasterManager.Instance.references.player.GetComponent<VignetteApplier>().FadeIn();
        MasterManager.Instance.references.player.transform.position = position.position;
        
        m_Collider.isTrigger = true;

        if (GetComponentInChildren<Renderer>() != null)
        {
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }
}
