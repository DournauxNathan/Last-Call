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
        MasterManager.OnPhaseChange += OnPhaseChange;
        _renderer = GetComponent<Renderer>();
        // if (MasterManager.Instance.currentPhase == Phases.Phase_3)
        // {
        //     headPhone.GetComponent<Rigidbody>().isKinematic = true;
        //     EquipHeadPhone();
        // }
    }
    private void OnDestroy() {
        MasterManager.OnPhaseChange -= OnPhaseChange; //Unsubscribe from event
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
    }

    private void OnPhaseChange(Phases phase){
        switch (phase)
        {
            case Phases.Phase_1:
                _renderer.enabled = true;
                break;
            case Phases.Phase_2:
               _renderer.enabled = false;
                break;
            case Phases.Phase_3:
                headPhone.GetComponent<Rigidbody>().isKinematic = true;
                EquipHeadPhone();
                break;
            default:
                Debug.LogError("Phase not found : "+ phase.ToString() +"\n"+this.gameObject);
                break;
        }
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
