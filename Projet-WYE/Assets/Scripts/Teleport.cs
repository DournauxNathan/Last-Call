using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Singleton<Teleport>
{
    private GameObject XrRig;
    public List<Transform> positionPoints;

    private void Start()
    {
        XrRig = GameObject.FindGameObjectWithTag("XRRig");
    }

    public void GoToPoint(int indexPoint)
    {
        XrRig.transform.position = positionPoints[indexPoint].position;
    }
}
