using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoRotateCam : MonoBehaviour
{
    [SerializeField] private GameObject xrRig;

    // Start is called before the first frame update
    void Start()
    {
        xrRig = MasterManager.Instance.xrRig;

        if (xrRig != null)
        {
            xrRig.transform.LookAt(this.transform);
            xrRig.transform.rotation = new Quaternion(0, xrRig.transform.rotation.y,0,xrRig.transform.rotation.w); 
        }
        else
        {
            Debug.LogError("No rig refenced");
        }
    }
}
