using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    [SerializeField]private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle1;

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("Debug");
            particle.Play();
            particle1.Play();
        }


    }
}
