using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhoneManager : MonoBehaviour
{
    public HeadPhone headPhone;
    public bool isOnHead;
    [SerializeField]public XRSocketInteractorWithAutoSetup socket;
    [SerializeField]private float offset= .3f;
    [Space(5)]
    public bool testBoolEquip;
    public bool equipBool;

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
       /* if (equipBool) // a enlever uniquement un test;
        {
            equipBool = false;
            headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f, offset, 0f);
        }*/

        if (MasterManager.Instance.currentPhase == Phases.Phase_2 || MasterManager.Instance.isInImaginary)
        {
            headPhone.GetComponent<Renderer>().enabled = false;
        }
        else if (MasterManager.Instance.currentPhase == Phases.Phase_1 || MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            headPhone.GetComponent<Renderer>().enabled = true; 

        }
    }

    public void AutoEquipHeadPhone()
    {
        if (!isOnHead && headPhone != null && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            Debug.Log("Put headset");
            isOnHead = true;
            headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f,offset,0f); // Fonctionne /!\ pas très propre
        }
    }

    public void HeadPhoneEquip()
    {
        isOnHead = !isOnHead;
        headPhone.isOnHead = isOnHead;

        if (isOnHead)
        {
            //SceneLoader.Instance.MoveGO(headPhone.gameObject);
        }/*
        else if (isOnHead)
        {

        }*/

        if (!isOnHead && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            MasterManager.Instance.ActivateImaginary("Appartment_Day1");
        }

    }
}
