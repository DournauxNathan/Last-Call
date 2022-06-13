using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateWheel : MonoBehaviour
{
    public static event Action<string, int> Rotated = delegate { };
    private bool coroutineAllowed;

    public bool rotate = false;
    public int currentFace;

    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
    }

    public void RotatePos()
    {
        rotate = true;

        if (rotate && coroutineAllowed && !GetComponentInParent<Padlock>().isComplete) 
        {
            rotate = false;
            StartCoroutine(DoRotate(3.65f, true));
        }
    }

    public void RotateNeg()
    {
        rotate = true;

        if (rotate && coroutineAllowed && !GetComponentInParent<Padlock>().isComplete)
        {
            rotate = false;
            StartCoroutine(DoRotate(-3.65f, false)) ;
        }   
    }

    public IEnumerator DoRotate(float value,bool increase)
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 9; i++)
        {
            transform.Rotate(0f, value, 0f, Space.Self);
            yield return new WaitForSeconds(0.01f);
        }

        coroutineAllowed = true;

        if (increase)
        {
            currentFace += 1;

            if (currentFace > 9)
            {
                currentFace = 0;
            }
        }
        else
        {
            currentFace -= 1;

            if (currentFace < 0)
            {
                currentFace = 9;
            }
        }


        Rotated(name, currentFace);
    }
}
