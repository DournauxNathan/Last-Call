using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;

public enum Phases
{
    Phase_0,
    Phase_1,
    Phase_2,
    Phase_3,
    Phase_4
}

public class MasterManager : Singleton<MasterManager>
{
    public Phases currentPhase;

    [Header("Refs")]
    public Transform cameraA;
    public XRInteractionManager xRInteractionManager;
    public GameObject EventSystem;
    public ObjectActivator objectActivator;
    public Projection projectionTransition;
    public AudioSource mainAudioSource;
    public Transform player;
    public HeadPhoneManager headsetManager;
    
    [Header("Hands")]
    public List<GameObject> baseInteractors;
    public List<GameObject> rayInteractors;

    [Header("Projection")]
     public bool canImagine = false;
    public bool isInImaginary;
    [HideInInspector] public bool pillsEffect;
    [Tooltip("Number of pills taken by the player")]
    [HideInInspector] public int currentPills = 0;

    [Header("Tutorial Management")]
    public bool skipTuto;
    public bool isTutoEnded;
    public bool startTuto;
    public float timerTutoBegin = 30f;
    private bool b = true;

    public UnityEvent startCall;

    [Header("Testing Input - Go in Projection")]
    public bool useOneInput = false;
    public bool useTwoInput = false;

    public GameObject xrRig;

    private void Start()
    {
        if (currentPhase == Phases.Phase_3)
        {
            /*for (int i = 0; i < UIManager.Instance.checkListTransform.childCount; i++)
            {
                UIManager.Instance.checkListTransform.GetChild(i).GetComponent<InstantiableButton>().button.enabled = false;
            }

            for (int i = 0; i < UIManager.Instance.descriptionTransform.childCount; i++)
            {
                UIManager.Instance.descriptionTransform.GetChild(i).GetComponent<InstantiableButton>().button.enabled = false;
            }*/

            UnitDispatcher.Instance.sequence = 4;
            UiTabSelection.Instance.SwitchSequence(UnitDispatcher.Instance.sequence);
        }

        UpdateController();
    }

    public void FixedUpdate()
    {
        UpdateController();

        if (!skipTuto && !isTutoEnded && b)
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

        if (skipTuto || isTutoEnded)
        {
            skipTuto = false;

            if (b)
            {
                b = false;
                startCall?.Invoke();
            }
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

    #region Dev Menu 
    public void SetIsInImaginary(bool b)
    {
        isInImaginary = b;
    }

    public void SetIsTutoSkip(bool b)
    {
        isTutoEnded = b;
    }
    #endregion

    public void UpdateController()
    {
        if (!isInImaginary)
        {
            for (int i = 0; i < rayInteractors.Count; i++)
            {
                baseInteractors[i].SetActive(true);
            }

            for (int i = 0; i < rayInteractors.Count; i++)
            {
                rayInteractors[i].SetActive(false);
            }
        }
        else if (isInImaginary)
        {
            for (int i = 0; i < baseInteractors.Count; i++)
            {
                baseInteractors[i].SetActive(false);
            }

            for (int i = 0; i < rayInteractors.Count; i++)
            {
                rayInteractors[i].SetActive(true);
            }
        }
    }

    public void ActivateImaginary(string name)
    {
        UpdateController();
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
        UIManager.Instance.PullQuestion();
    }


    public void SetPhase(int i)
    {
        switch (i)
        {
            case 0:
                currentPhase = Phases.Phase_0;
                break;
            case 1:
                currentPhase = Phases.Phase_1;
                break;
            case 2:
                currentPhase = Phases.Phase_2;
                break;
            case 3:
                currentPhase = Phases.Phase_3;
                break;
            case 4:
                currentPhase = Phases.Phase_4;
                break;
        }
    }
}
