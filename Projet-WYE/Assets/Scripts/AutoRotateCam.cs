using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRotateCam : MonoBehaviour
{
    private Transform xrRig;

    public bool constraintToCam;

    // Start is called before the first frame update
    void Start()
    {
        xrRig = MasterManager.Instance.references.player;

        if (xrRig != null)
        {
            xrRig.LookAt(this.transform);
            xrRig.transform.rotation = new Quaternion(0, xrRig.transform.rotation.y,0,xrRig.transform.rotation.w); 
        }
        else
        {
            Debug.LogError("No rig refenced");
        }
    }

    private void LateUpdate()
    {
        if (constraintToCam)
        {
            xrRig.LookAt(this.transform);
            xrRig.transform.rotation = new Quaternion(0, xrRig.transform.rotation.y, 0, xrRig.transform.rotation.w);
        }
    }

}
