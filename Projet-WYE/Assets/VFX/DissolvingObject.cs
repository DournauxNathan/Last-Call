using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class DissolvingObject : MonoBehaviour
{

    public MeshRenderer MeshRenderer;
    public float dissolveRate = 0.02f;
    public float refreshRate = 0.05f;
    public VisualEffect VFXGraph;
    public ParticleSystem Particle;
    public float Delay = 0.02f;

    private Material[] dissolveMaterials;
    // Start is called before the first frame update
    void Start()
    {
        if (VFXGraph != null)
        {
           VFXGraph.Stop();
           VFXGraph.gameObject.SetActive(false);
        }

        if (MeshRenderer != null)
            dissolveMaterials = MeshRenderer.materials;

        if (Particle != null)
        {
            Particle.Stop();
            Particle.gameObject.SetActive(false);
        }

        if (MeshRenderer != null)
            dissolveMaterials = MeshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(Dissolve());
        }       
    }

    IEnumerator Dissolve()
    {
        if (VFXGraph != null)
        {
            VFXGraph.gameObject.SetActive(true);
            VFXGraph.Play();
        }
        if (Particle != null)
        {
            Particle.gameObject.SetActive(true);
            Particle.Play();
        }

        float counter = 0;

        if (dissolveMaterials.Length > 0)
        {
            while (dissolveMaterials[0].GetFloat("DissolveAmount_") < 1)
            {
                counter += dissolveRate;

                for (int i = 0; i < dissolveMaterials.Length; i++)
                {
                    dissolveMaterials[i].SetFloat("DissolveAmount_", counter);
                } 

                yield return new WaitForSeconds(refreshRate);
            }
        }

        Destroy(gameObject, 1);
    }
}

