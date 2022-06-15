
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeadPhone : MonoBehaviour
{
    private HeadPhone headPhone;
    private HeadPhoneManager manager;

    public bool isOnHead;
    public UnityEvent onHead;

    private bool b = true;

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
            b = !b;

            SetUp();
        }
    }


    private void SetUp()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            manager = MasterManager.Instance.references.headsetManager;
            manager.GetComponent<HeadPhoneManager>();
            manager.headPhone = GetHeadPhone();


            GetComponent<Renderer>().enabled = true;
            GetComponentInChildren<CapsuleCollider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private ref HeadPhone GetHeadPhone()  { return ref headPhone; }

    public void DisableHeadset()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponentInChildren<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnHead()
    {
        if (isOnHead)
        {
            onHead?.Invoke();
        }
    }
}
