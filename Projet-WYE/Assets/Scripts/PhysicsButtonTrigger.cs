using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButtonTrigger : MonoBehaviour
{
    public UnityEvent onPressed;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.CompareTag("Button"));

        if (other.CompareTag("Button"))
        {
            onPressed?.Invoke();
        }
    }
}
