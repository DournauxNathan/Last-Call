using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBottle : MonoBehaviour
{
    public GameObject particle;
    public GameObject particle1;

    public float rotationX = 90f;
    public float rotationZ = 90f;

    // Start is called before the first frame update
    void Start()
    {
        particle.SetActive(false);
        particle1.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //condition to check if the bottle is being held
        bool xPosP = WrapAngle(transform.localEulerAngles.x) > rotationX && WrapAngle(transform.localEulerAngles.x) > 0;
        bool xPosM = WrapAngle(transform.localEulerAngles.x) < -rotationX && WrapAngle(transform.localEulerAngles.x) < 0;
        bool zPosP = WrapAngle(transform.localEulerAngles.z) > rotationZ && WrapAngle(transform.localEulerAngles.z) > 0;
        bool zPosM = WrapAngle(transform.localEulerAngles.z) < -rotationZ && WrapAngle(transform.localEulerAngles.z) < 0;

        if (xPosP || xPosM)
        {
            particle.SetActive(true);
            particle1.SetActive(true);

            //Debug.Log("X "+xPosP+" "+xPosM);
        }
        else if (zPosP || zPosM)
        {
            particle.SetActive(true);
            particle1.SetActive(true);

            //Debug.Log("Z "+zPosP+" "+zPosM);  
        }
        else if (particle.activeSelf)
        {
            StartCoroutine(ExampleCoroutine());

            particle.SetActive(false);

        }
    }

    private static float WrapAngle(float angle) //returns angle between -180 and 180
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private static float UnwrapAngle(float angle) //returns angle between 0 and 360
    {
        angle %= 360;
        if (angle < 0)
            return angle + 360;

        return angle;
    }

    IEnumerator ExampleCoroutine()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        particle1.SetActive(false);

    }
}