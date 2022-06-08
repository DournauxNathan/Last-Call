using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObj : MonoBehaviour
{
    [SerializeField] private GameObject fracturedObject;
    public List<string> collisionTags;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.CompareTag(collisionTags[0]))
        {
            Instantiate(fracturedObject, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
