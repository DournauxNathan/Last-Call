using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    private Vector3 v3_Original;
    private Vector3 q_rotaion;
    private Rigidbody rb;
    [SerializeField]
    private string colliderR;
    [SerializeField]
    private float offset = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v3_Original = gameObject.transform.position;
        q_rotaion = gameObject.transform.eulerAngles;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(colliderR) || other.CompareTag("Trash"))
        {
            rb.velocity = Vector3.zero;
            gameObject.transform.eulerAngles = q_rotaion;
            gameObject.transform.position = v3_Original + new Vector3(0,offset,0);
        }
    }
}
