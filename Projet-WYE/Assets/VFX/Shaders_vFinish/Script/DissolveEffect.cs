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

    public void Init(bool hasAlreadyParticle)
    {
        if (!hasAlreadyParticle)
        {
#if UNITY_EDITOR
            particlePrefab = PrefabUtility.InstantiatePrefab(Resources.Load("Prefabs/VFX_Start")) as GameObject;
#endif
            particlePrefab.transform.position = Vector3.zero;
            particlePrefab.transform.localEulerAngles = Vector3.zero;

            particlePrefab.transform.SetParent(this.transform);


            particles = particlePrefab.GetComponent<ParticleSystem>();
        }
        else
        {
            //Debug.Log(this.gameObject.name + " has already a particle Prefab. \n Reset position and rotation");
        }

        if (particles != null)
        {
            particles.gameObject.SetActive(false);
            particles.Stop();
        }
        else
        {
            Debug.Log("There is no particles in " + this.gameObject.name + ", Object reference is missing in Inspector");
        }
    }

    private void Start()
    {
        if (TryGetComponent<XRGrabInteractableWithAutoSetup>(out XRGrabInteractableWithAutoSetup xrGrab))
        {
            xrGrab.enabled = false;
        }

        if (TryGetComponent<XRSimpleInteractableWithAutoSetup>(out XRSimpleInteractableWithAutoSetup xrSimple))
        {
            xrSimple.enabled = false;
        }        
    }
    public bool doOnce;

    private void FixedUpdate()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_2)
        {
            if (startEffect)
            {
                startEffect = !startEffect;
                StartCoroutine(Dissolve());
            }

            if (GetComponent<Renderer>().sharedMaterial.GetFloat("_Dissolve") > 1)
            {
                GetComponent<CombinableObject>().ToggleInteractor(true);

                Reveal();
            }
        }
    }

    public void Reveal()
    {
        if (TryGetComponent<Renderer>(out Renderer rend))
        {
            rend.enabled = true;
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

        if (GetComponent<Renderer>().sharedMaterials.Length > 0)
        {
            while (GetComponent<Renderer>().sharedMaterial.GetFloat("_Dissolve") > 1)
            {
                counter -= Time.deltaTime * dissolveRate;

                for (int i = 0; i < GetComponent<Renderer>().sharedMaterials.Length; i++)
                {
                    GetComponent<Renderer>().sharedMaterial.SetFloat("_Dissolve", counter);
                }

                if (GetComponent<Renderer>().sharedMaterial.GetFloat("_Dissolve") <= 0)
                {
                    for (int i = 0; i < GetComponent<Renderer>().sharedMaterials.Length; i++)
                    {
                        if (TryGetComponent<Renderer>(out Renderer rend))
                        {
                            rend.enabled = false;
                        }

                        if (TryGetComponent<XRGrabInteractableWithAutoSetup>(out XRGrabInteractableWithAutoSetup xrGrab))
                        {
                            xrGrab.enabled = false;
                        }

                        if (TryGetComponent<XRSimpleInteractableWithAutoSetup>(out XRSimpleInteractableWithAutoSetup xrSimple))
                        {
                            xrSimple.enabled = false;
                        }
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

