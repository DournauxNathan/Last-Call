using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallManager : Singleton<CallManager>
{

    [SerializeField] private float timeToCall = 20f;
    [SerializeField] private Calls phone;
    [SerializeField] private bool hasCalled = false;

    [SerializeField] public bool enableCall = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enableCall)
        {
            if (timeToCall > 0 && !hasCalled)
            {
                timeToCall -= Time.deltaTime;
            }
            else if (!hasCalled)
            {
                hasCalled = true;
                phone.triggerCall = true;
            }
        }
    }
}
