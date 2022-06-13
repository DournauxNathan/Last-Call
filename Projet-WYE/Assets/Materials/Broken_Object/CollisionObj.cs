using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObj : MonoBehaviour
{
    [SerializeField] private GameObject fracturedObject;
    public List<string> collisionTags;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyy"))
        {
            Debug.Log(other.gameObject.name);
            
            Instantiate(fracturedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
