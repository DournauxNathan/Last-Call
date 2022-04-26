using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using UnityEditor;

public class DissolveEffect : Singleton<DissolveEffect>
{
    [Header("Refs")]
    private GameObject particlePrefab;
    public ParticleSystem particles;

    [Header("Properties")]
    public float dissolveRate = 0.8f;
    public float refreshRate = 0.05f;
    public float delay = 0.02f;
    public bool startEffect = false;

    public Material[] dissolveMaterials;

    public void Init()
    {
#if UNITY_EDITOR
        particlePrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VFX Start")) as GameObject;
#endif
        particlePrefab.transform.SetParent(this.transform);

        particles = particlePrefab.GetComponent<ParticleSystem>();
            

        if (particles != null)
        {
            particles.gameObject.SetActive(false);
            particles.Stop();
        }
        else
        {
            Debug.LogWarning("There is no particles, Object reference is missing in Inspector");
        }

    }

    private void FixedUpdate()
    {
        if (startEffect)
        {
            startEffect = !startEffect;
            StartCoroutine(Dissolve());
        }
    }

    public IEnumerator Dissolve()
    {
        if (particles != null)
        {
            particles.gameObject.SetActive(true);
            particles.Play();
        }
        else
        {
            Debug.LogWarning("There is no particles, Object reference is missing in Inspector");
        }

        float counter = 50;

        if (dissolveMaterials.Length > 0)
        {
            while (dissolveMaterials[0].GetFloat("_Dissolve") > 1)
            {
                counter -= Time.deltaTime * dissolveRate;

                for (int i = 0; i < dissolveMaterials.Length; i++)
                {
                    dissolveMaterials[i].SetFloat("_Dissolve", counter);
                }

                if (dissolveMaterials[0].GetFloat("_Dissolve") <= 0)
                {
                    for (int i = 0; i < dissolveMaterials.Length; i++)
                    {
                        GetComponent<Renderer>().enabled = false;
                        GetComponent<CombinableObject>().enabled = false;
                    }
                }

                yield return new WaitForSeconds(refreshRate);
            }
        }
        else
        {
            Debug.LogWarning("List is empty");
        }


        //gameObject.SetActive(false);
    }
}

