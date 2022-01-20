using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MasterManager : Singleton<MasterManager>
{
    [Header("Refs")]
    public XRInteractionManager xRInteractionManager;
    public ObjectActivator objectActivator;
    public Projection projectionTransition;
    public AudioSource mainAudioSource;

    public List<GameObject> rayInteractors;

    [Header("Metrics")]
    public bool isInImaginary;
    public bool pillsEffect;
    public bool isTutoEnded;
    [Tooltip("Number of pills taken by the player")]
    public int currentPills = 0;

    private void Start()
    {
        UpdateController();

        }

    void EffectOfPills()
    {
        if (currentPills == 1)
        {
            //Expand the timer of the call
            /* The time is ""slow"", the events of the call arrived less faster ? */

            //Active  Interactor, outline of useless objets
            objectActivator.ToggleUselessObject(true, 3);
        }
        else if (currentPills > 1)
        {
            objectActivator.ToggleUselessObject(true, 3);
        }
    }

    public void UpdateController()
    {
        if (!isInImaginary)
        {
            for (int i = 0; i < rayInteractors.Count; i++)
            {
                rayInteractors[i].SetActive(false);
            }
        }
        else if (isInImaginary)
        {
            for (int i = 0; i < rayInteractors.Count; i++)
            {
                rayInteractors[i].SetActive(true);
            }
        }
    }

    public void ActivateImaginary(string name)
    {
        objectActivator.ActivateObjet();
        SceneLoader.Instance.LoadNewScene(name);

    }

    public void GoBackToScene(string name)
    {
        isTutoEnded = true;
        projectionTransition.startTransition = true;
        //ScenarioManager.Instance.LoadScenario();

        isInImaginary = false;
        SceneLoader.Instance.LoadNewScene(name);
    }

}
