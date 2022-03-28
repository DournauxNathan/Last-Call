using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhone : MonoBehaviour
{
    private HeadPhone headPhone;
    private HeadPhoneManager manager;

    public bool isOnHead;

    public bool b = true;

    // Start is called before the first frame update
    void Start()
    {
        headPhone = this;

        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

    }
    private void LateUpdate()
    {
        if (b && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            b = false;

            SetUp();
        }
    }


    private void SetUp()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            manager = MasterManager.Instance.headsetManager;
            manager.GetComponent<HeadPhoneManager>();
            manager.headPhone = GetHeadPhoneRef();


            GetComponent<Renderer>().enabled = true;
            GetComponentInChildren<CapsuleCollider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private ref HeadPhone GetHeadPhoneRef()
    {
        return ref headPhone; 
    }


    public void GetInteractor()
    {

    }

    public void DisableHeadset()
    {

        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }


}
