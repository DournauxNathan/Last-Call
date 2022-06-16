using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMover : MonoBehaviour
{
    private Transform xRRig;
    public float distance;
    private void Start() {
        xRRig = MasterManager.Instance.references.xRRig.transform;
    }

    public void AvancerRig(){
        xRRig.transform.position = new Vector3(xRRig.position.x, xRRig.position.y, xRRig.position.z + distance);
    }
    public void ReculerRig(){
        xRRig.transform.position = new Vector3(xRRig.position.x, xRRig.position.y, xRRig.position.z - distance);
    }
    public void DroiteRig(){
        xRRig.transform.position = new Vector3(xRRig.position.x + distance, xRRig.position.y, xRRig.position.z);
    }
    public void GaucheRig(){
        xRRig.transform.position = new Vector3(xRRig.position.x - distance, xRRig.position.y, xRRig.position.z);
    }

}
