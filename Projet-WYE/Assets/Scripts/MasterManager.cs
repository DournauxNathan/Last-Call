using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
    public References references;
    
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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneLoader.Instance.LoadNewScene("Office");
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            WordManager.Instance.isProtocolComplete = true;
        }


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
            for (int i = 0; i < references.rayInteractors.Count; i++)
            {
                references.baseInteractors[i].SetActive(true);
            }

            for (int i = 0; i < references.rayInteractors.Count; i++)
            {
                references.rayInteractors[i].SetActive(false);
            }
        }
        else if (isInImaginary)
        {
            for (int i = 0; i < references.baseInteractors.Count; i++)
            {
                references.baseInteractors[i].SetActive(false);
            }

            for (int i = 0; i < references.rayInteractors.Count; i++)
            {
                references.rayInteractors[i].SetActive(true);
            }
        }
    }

    public void ActivateImaginary(string name)
    {
        SetPhase(2);
        SceneLoader.Instance.LoadNewScene(name);
        UpdateController();
        WordManager.Instance.PullWord();
    }

    public void GoBackToOffice(string name)
    {
        SetPhase(3);
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

[System.Serializable]
public class References
{
    [Header("XR")]
    public XRInteractionManager xRInteractionManager;
    public GameObject _RRig;
    public List<GameObject> baseInteractors;
    public List<GameObject> rayInteractors;


    [Header("Player")]
    public Transform mainCamera;
    public Transform player;
    public Projection projectionTransition;
    public AudioSource mainAudioSource;

    [Header("Puzzle Manager")]
    public ListManager _listManager;
    public OrderController _orderController;

    [Header("UI & Event system")]
    public GameObject eventSystem;

    [Header("Others")]
    public HeadPhoneManager headsetManager;
}
