using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhoneManager : Singleton<HeadPhoneManager>
{
    public HeadPhone headPhone;
    public bool isOnHead;
    [SerializeField]public XRSocketInteractorWithAutoSetup socket;
    [SerializeField]private float offset= .3f;
    [Space(5)]
    
    public bool equip;

    public bool press;
    [SerializeField] private OnTriggerEvents triggerEvents;
    [SerializeField] private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        // if (MasterManager.Instance.currentPhase == Phases.Phase_3)
        // {
        //     headPhone.GetComponent<Rigidbody>().isKinematic = true;
        //     EquipHeadPhone();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // if (MasterManager.Instance.currentPhase == Phases.Phase_2 || MasterManager.Instance.isInImaginary)
        // {
        //     headPhone.GetComponent<Renderer>().enabled = false;
        // }
        // else if (MasterManager.Instance.currentPhase == Phases.Phase_1 || MasterManager.Instance.currentPhase == Phases.Phase_3)
        // {
        //     headPhone.GetComponent<Renderer>().enabled = true; 
        // }

        if (press)
        {
            press = !press;
            triggerEvents.triggerEnter?.Invoke();

        }

        if (equip)
        {
            equip = !equip; 
            Equip(true);
        }
        OnPhaseChange((int)MasterManager.Instance.currentPhase);
    }

    public bool doOnce = true;

    public void OnPhaseChange(int phase)
    {
        switch (phase)
        {
            case 0:
                _renderer.enabled = false;
                headPhone.GetComponent<Rigidbody>().isKinematic = true;
                headPhone.GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
                break;
            case 1:
                doOnce = true;
                _renderer.enabled = true;
                headPhone.GetComponent<Rigidbody>().isKinematic = false;
                headPhone.GetComponent<XRGrabInteractableWithAutoSetup>().enabled = true;
                break;
            case 2:
               _renderer.enabled = false;
                break;
            case 3:

                if (doOnce)
                {
                    EquipHeadPhone();

                    _renderer.enabled = true;
                    headPhone.GetComponent<XRGrabInteractableWithAutoSetup>().enabled = true;
                    _renderer.sharedMaterial.SetFloat("_Dissolve", 50f);
                    headPhone.GetComponent<Rigidbody>().isKinematic = false;

                    doOnce = false;
                }
                break;
        }

        //Debug.Log(phase);
    }

    public void EquipHeadPhone()
    {
        headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f, offset, 0f); // Fonctionne /!\ pas tr�s propre

        //headPhone.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void SetEquip(bool value) { equip = value; }
    public void Press(bool value) { press = value; }

    public void Equip(bool value)
    {
        headPhone.isOnHead = value;

        if (value && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            EquipHeadPhone();
            headPhone.onHead?.Invoke();
        }

        if (!value && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            Debug.Log("Headset off");
            this.CallWithDelay(OffHead, 8);
        }
    }

    public void OffHead()
    {
        Debug.Log("Loading");
        AppartManager.Instance.LoadAppartOnScenarioEnd();
    }
}
