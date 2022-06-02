using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using TMPro;

public enum Phases
{
    Phase_0 = 0,
    Phase_1 = 1,
    Phase_2 = 2,
    Phase_3 = 3,
    Phase_4 = 4
}

public class MasterManager : Singleton<MasterManager>
{
    public bool unpauseAdio;
    public Phases currentPhase;

    [Header("Refs")]
    public References references;

    [Header("Call")]
    public bool isEnded; 


    [Header("Projection")]
     public bool canImagine = false;
    public bool isInImaginary;

    [Header("Tutorial Management")]
    public bool skipTuto;
    public bool isTutoEnded;
    public bool startTuto;
    public float timerTutoBegin = 30f;
    private bool b = true;

    public UnityEvent startCall;

    public int buttonEmissive;
    public TMP_Text text;
    public TMP_Text text1;
    public bool aCoup = true;

    private void Start()
    {
        InitializeLevel();
    }

    private void Update()
    {
        //To put into the debug menu
        if (Keyboard.current.f12Key.wasPressedThisFrame)
        {
            OrderController.Instance.ResolvePuzzle();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        //To put into the debug menu
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            WordManager.Instance.isProtocolComplete = true;
        }

        if (unpauseAdio)
        {
            unpauseAdio = !unpauseAdio;
            MasterManager.Instance.references.mainAudioSource.UnPause();
        }

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
                //startCall?.Invoke();
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

    public void ChangeSceneByName(int value, string name)
    {
        SceneLoader.Instance.LoadNewScene(name);
        SetPhase(value);
    }

    public void AddSceneByName(int value, string name)
    {
        SceneLoader.Instance.AddNewScene(name);
        SetPhase(value);
    }

    public void StartTuto()
    {
        CallManager.Instance.enableCall = true;
    }

    public void StartCall()
    {
        isTutoEnded = true;
    }

    public void InitializeLevel()
    {
        //ScenarioManager.Instance.LoadScenario();
        UpdateController();
        SetPhase(currentPhase);
    }

    public void SetPhase(Phases phase)
    {
        switch (currentPhase)
        {
            case Phases.Phase_0:
                SetPhase(0);
                break;

            case Phases.Phase_1:
                SetPhase(1);
                break;

            case Phases.Phase_2:
                SetPhase(2);
                break;

            case Phases.Phase_3:
                SetPhase(3);
                break;

            case Phases.Phase_4:
                SetPhase(4);
                break;
        }
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

        SetupPhase(i);
    }

    private void SetupPhase(int i)
    {
        switch (i)
        {
            case 0:
                Projection.Instance.SetTransitionValue(0);
                break;

            case 1:
                Projection.Instance.enableTransition = true;
                Projection.Instance.transitionValue = 50f;
                ScenarioManager.Instance.UpdateScenario(1);
                TimeSettings.Instance.Initialize();
                UpdateController();
                break;

            case 2:
                if (!Projection.Instance.onEditor)
                {
                    Projection.Instance.transitionValue = 0f;
                }
                MasterManager.Instance.isInImaginary = true;
                UpdateController();
                WordManager.Instance.PullWord();

                Projection.Instance.SetTransitionValue(0);
                Projection.Instance.enableTransition = false;
                break;

            case 3:
                Projection.Instance.enableTransition = true;
                Projection.Instance.SetTransitionValue(50);
                this.CallWithDelay(CallEnded, 5);

                isTutoEnded = true;
                //isInImaginary = false;
                //Projection.Instance.revealScene = true;

                WordManager.Instance.PullWord();
                UIManager.Instance.UpdateUnitManager(4);
                break;

            case 4:
                //ScenarioManager.Instance.UpdateScenario(1);
                Reset();
                break;
        }

    }


    public void Reset()
    {
        isInImaginary = false;

        Projection.Instance.transitionValue = 50f;
        Projection.Instance.enableTransition = true;

        currentPhase = Phases.Phase_0;
        ScenarioManager.Instance.currentScenarioData = null;
        isEnded = false;
        OrderController.Instance.ordersStrings.Clear();
        OrderController.Instance.combinaisons.Clear();
        OrderController.Instance.puzzlesSucced = 0;
        OrderController.Instance.isResolve = false;

        UnitManager.Instance.physicsbuttons.Clear();

        WordManager.Instance.answers.Clear();
        WordManager.Instance.questions.Clear();
    }
    public void PlayDialogues()
    {
        if (!references.mainAudioSource.isPlaying && currentPhase == Phases.Phase_1)
        {
            references.mainAudioSource.PlayNewClipOnce(ScenarioManager.Instance.currentScenarioData.dialogues);
            this.CallWithDelay(WordManager.Instance.PullWord, ScenarioManager.Instance.currentScenarioData.timeAfterDialogueBegins);
            UIManager.Instance.InComingCall(false);

            TimeSettings.Instance.StartGlobalTimer();
        }
    }

    public void CallEnded()
    {
        isEnded = true;
        UIManager.Instance.OutComingCall(true);
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
    public AudioMixerGroup sfx;

    [Header("Puzzle Manager")]
    public ListManager _listManager;
    public OrderController _orderController;

    [Header("UI & Event system")]
    public GameObject eventSystem;

    [Header("Others")]
    public HeadPhoneManager headsetManager;
}
