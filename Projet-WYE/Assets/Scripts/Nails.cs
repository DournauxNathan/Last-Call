using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Nails : MonoBehaviour
{
    public int count;
    public Transform t1, t2;

    public XRSocketInteractorWithAutoSetup socket;

    public UnityEvent drive;
    public UnityEvent done;

    public void Drive()
    {
        count++;
        SetSocket(false);

        if (count == 1)
        {
            //transform.position += Vector3.down;
            SetSocket(true);

            
            socket.transform.position = t1.position;
        }
        if (count >= 2)
        {
            SetSocket(true);
            count = 2;
            socket.transform.position = t2.position;
            done?.Invoke();

            GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
        }
    }

    public void SetSocket(bool value)
    {
        socket.enabled = value;
    }

    public void GetSocket()
    {
    }

}
