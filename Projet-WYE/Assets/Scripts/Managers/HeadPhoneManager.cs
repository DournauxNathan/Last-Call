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

    private void Awake()
    {
        if (!isOnHead && headPhone != null && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            AutoEquipHeadPhone();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_2 || MasterManager.Instance.isInImaginary)
        {
            headPhone.GetComponent<Renderer>().enabled = false;
        }
        else if (MasterManager.Instance.currentPhase == Phases.Phase_1 || MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            headPhone.GetComponent<Renderer>().enabled = true; 
        }

        if (press)
        {
            press = !press;
            triggerEvents.triggerEnter?.Invoke();

        }

        if (equip)
        {
            equip = !equip;
            Equip(true); 
            EquipHeadPhone();
        }
    }

    public void AutoEquipHeadPhone()
    {
        if (!isOnHead && headPhone != null && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            EquipHeadPhone();
        }
    }

    public void EquipHeadPhone()
    {
        headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f, offset, 0f); // Fonctionne /!\ pas très propre
    }


    public void Equip(bool value)
    {
        headPhone.isOnHead = value;

        if (value && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            headPhone.onHead?.Invoke();
        }

        if (!value && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            this.CallWithDelay(OffHead, 15);
        }
    }

    public void OffHead()
    {
        MasterManager.Instance.SetPhase(4);
        AppartManager.Instance.LoadAppartOnScenarioEnd();
    }
}
