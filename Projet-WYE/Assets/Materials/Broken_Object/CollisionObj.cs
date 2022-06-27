using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObj : MonoBehaviour
{
    [SerializeField] private GameObject fracturedObject;
    public List<string> collisionTags;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(collisionTags[0]) || other.CompareTag(collisionTags[1]))
        {
            Debug.Log(other.gameObject.name);
            
            Instantiate(fracturedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
