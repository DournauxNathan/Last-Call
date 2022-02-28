using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    private Transform position;
    public bool teleport;

    private void Start()
    {
        position = this.transform;
    }

    private void Update()
    {
        if (teleport)
        {
            TeleportTo();
            teleport = !teleport;
        }
    }

    internal void TeleportTo()
    {
        MasterManager.Instance.player.GetComponent<VignetteApplier>().FadeIn();
           MasterManager.Instance.player.transform.position = position.position;
    }
}
