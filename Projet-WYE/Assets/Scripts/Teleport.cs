using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    public Transform position;

    private void Start()
    {
        position = this.transform;
    }

    internal void TeleportTo()
    {
        MasterManager.Instance.player.transform.position = position.position;
    }
}
