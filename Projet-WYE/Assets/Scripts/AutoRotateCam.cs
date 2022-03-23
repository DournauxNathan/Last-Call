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

        }
        else
        {
            Debug.LogError("No rig refenced");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
