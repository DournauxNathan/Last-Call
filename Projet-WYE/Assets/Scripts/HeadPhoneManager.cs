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

    // Update is called once per frame
    void Update()
    {
        if (equipBool) // a enlever uniquement un test;
        {
            equipBool = false;
            headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f, offset, 0f);
        }
    }

    public void AutoEquipHeadPhone()
    {
        if (isOnHead&&headPhone !=null)
        {
            isOnHead = false;
            headPhone.gameObject.transform.position = socket.transform.position + new Vector3(0f,offset,0f); // Fonctionne /!\ pas très propre
        }
    }

    public void HeadPhoneEquip()
    {
        isOnHead = !isOnHead;
        headPhone.isOnHead = isOnHead;

        if (isOnHead)
        {
            Debug.Log("Start Call Here !");
        }
        else if (!isOnHead /* &&  autre condition  */)
        {
            Debug.Log("Fin de journée");
        }
    }
}
