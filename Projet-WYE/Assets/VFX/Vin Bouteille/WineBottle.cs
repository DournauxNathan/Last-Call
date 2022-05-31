using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineBottle : MonoBehaviour
{
    public Waterenmoins water;
    public GameObject particle;
    public GameObject particle1;

    public float rotationX = 90f;
    public float rotationY = 90f;

    public Vector3 posInitial;


    // Start is called before the first frame update
    void Start()
    {
            particle.SetActive(true);
            particle1.SetActive(true);

            //Debug.Log("X "+xPosP+" "+xPosM);
        
        posInitial = particle1.transform.position;

        particle.SetActive(false);
        particle1.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //condition to check if the bottle is being held
        bool xPosP = WrapAngle(transform.localEulerAngles.x) > rotationX && WrapAngle(transform.localEulerAngles.x) > 0;
        bool xPosM = WrapAngle(transform.localEulerAngles.x) < -rotationX && WrapAngle(transform.localEulerAngles.x) < 0;
        bool yPosP = WrapAngle(transform.localEulerAngles.y) > rotationY && WrapAngle(transform.localEulerAngles.y) > 0;
        bool yPosM = WrapAngle(transform.localEulerAngles.y) < -rotationY && WrapAngle(transform.localEulerAngles.y) < 0;

        if (xPosP || xPosM)
        {
            water.test = true;
            particle.SetActive(true);
            particle1.SetActive(true);
            
            //Debug.Log("X "+xPosP+" "+xPosM);
        }

        else if (yPosP || yPosM)
        {
            water.test = true;
            particle.SetActive(true);
            particle1.SetActive(true);

            //Debug.Log("Z "+zPosP+" "+zPosM);  
        }
        else if (particle.activeSelf)
        {
            StartCoroutine(ExampleCoroutine());

            particle.SetActive(false);
            particle1.transform.position = particle.transform.position;
            water.test = false;
        }

        if (water.descente <= -0.2)
        {
            water.test = true;
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
        yield return new WaitForSeconds(1);

        particle1.SetActive(false);

    }
}