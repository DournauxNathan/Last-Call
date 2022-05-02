using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Nails : MonoBehaviour
{
    public float offset;

    public int count;
    public Transform t1, t2;

    public UnityEvent done;

    public void Drive()
    {
        count++;

        if (count == 1)
        {
            //transform.position += Vector3.down;

            transform.position = t1.position;
        }
        if (count >= 2)
        {
            count = 2;
            transform.position = t2.position;
            done?.Invoke();
        }
    }
    
}
