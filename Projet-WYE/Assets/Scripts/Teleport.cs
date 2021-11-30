using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    public GameObject rig;
    public Transform newTpPoint;
    public Transform returnPoint;


    public void EnableRig()
    {
        rig.transform.position = newTpPoint.position;
    }

    public void ReturnToOffice()
    {
        rig.transform.position = returnPoint.position;
    }
}
