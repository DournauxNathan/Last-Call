using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;

public class MasterManager : Singleton<MasterManager>
{
    [Header("Refs")]
    public XRInteractionManager xRInteractionManager;
    public ObjectActivator objectActivator;
    public Projection projectionTransition;
    public AudioSource mainAudioSource;

    public List<GameObject> rayInteractors;

    [Header("Projection and Pills Management")]
    public bool canImagine = false;
    public bool isInImaginary;
    public bool pillsEffect;
    [Tooltip("Number of pills taken by the player")]
    public int currentPills = 0;

    [Header("Tutorial Management")]
    public bool skipTuto;
    public bool isTutoEnded;
    public bool startTuto;
    public float timerTutoBegin = 30f;

    public UnityEvent startCall;

    [Header("Testing Input - Go in Projection")]
    public bool useOneInput = false;
    public bool useTwoInput = false;

    private void Start()
    {
        UpdateController();
    }

    public void FixedUpdate()
    {
        if (!skipTuto && !isTutoEnded)
        {
            timerTutoBegin -= Time.deltaTime;
        }

        if (timerTutoBegin <= 0)
        {
            startTuto = true;
        }

        if (startTuto)
        {
            StartTuto();
            timerTutoBegin = 0;
            startTuto = false;
        }

        if (skipTuto)
        {
            skipTuto = false;
            StartCall();
        }
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

    public void GoBackToOffice(string name)
    {
        isTutoEnded = true;
        isInImaginary = false;
        SceneLoader.Instance.LoadNewScene(name);
    }

    public void StartTuto()
    {
        CallManager.Instance.enableCall = true;
    }

    public void StartCall()
    {
        isTutoEnded = true;
        startCall.Invoke();
    }

}
