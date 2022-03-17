using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticle : MonoBehaviour
{
    [SerializeField]private ParticleSystem particle;
    [SerializeField] private ParticleSystem particle1;

    public int i;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            particle.Play();
            particle1.Play();
            i++;

            if (i == 3)
            {
                MasterManager.Instance.startTuto = true;
            }
        }
    }
}
