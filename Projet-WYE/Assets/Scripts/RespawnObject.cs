using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectManager))]

public class RespawnObject : MonoBehaviour
{
    private Vector3 v3_Original;
    private Rigidbody rb;
    [SerializeField]
    private string colliderR;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v3_Original = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag(colliderR))
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.position = v3_Original;

        }
    }
}
