using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Break : MonoBehaviour
{
    public GameObject fractured;
    public float breakForce;
    public bool isPress = false;

    void FixedUpdate()
    {

        Keyboard kb = Keyboard.current;

        if (isPress)
        {
            Debug.Log("Press");
            BreakTheThing();
        }
    }
    public void BreakTheThing(){
        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);

        foreach(Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>()){
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }
        Destroy(gameObject);
    }
}
