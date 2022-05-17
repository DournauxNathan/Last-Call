using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObj : MonoBehaviour
{
    [SerializeField] private GameObject fracturedObject;
    public List<string> collisionTags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        foreach (var tag in collisionTags)
        {
            if (other.tag == tag)
            {
                Instantiate(fracturedObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
