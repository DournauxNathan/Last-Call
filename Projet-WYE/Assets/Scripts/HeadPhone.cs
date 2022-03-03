using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhone : MonoBehaviour
{
    private HeadPhone headPhone;
    private HeadPhoneManager manager;

    public bool isOnHead;


    // Start is called before the first frame update
    void Start()
    {
        headPhone = this;
        SetUp();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUp()
    {
        manager = FindObjectOfType<HeadPhoneManager>();
        manager.GetComponent<HeadPhoneManager>();
        manager.headPhone = GetHeadPhoneRef();
    }

    private ref HeadPhone GetHeadPhoneRef()
    {
        return ref headPhone; 
    }


    public void GetInteractor()
    {

    }



}
