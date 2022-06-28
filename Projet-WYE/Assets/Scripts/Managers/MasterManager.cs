using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using TMPro;
using System;

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
    public bool hasSeenIntro = false;
    public bool displayIntro = false;
    public bool unpauseAdio;
    public Phases currentPhase;

    [Header("Refs")]
    public References references;

    [Header("Call")]
    public bool isEnded;

    public float offsetForCamera;

    [Header("Projection")]
    public bool envIsReveal;
    public bool canImagine = false;
    public bool isInImaginary;

    [Header("Tutorial Management")]
    public bool skipTuto;
    public bool skipIntro;
    public bool isTutoEnded;
    public bool startTuto;
    public float timerTutoBegin = 30f;
    private bool b = true;

    public UnityEvent startCall;

    public int buttonEmissive;
    public TMP_Text text;
    public TMP_Text text1;
    public bool aCoup = true;

    public static event Action<Phases> OnPhaseChange;

    private void Start()
    {
        InitializeLevel();
    }

    private void Update()
    {

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


        //To put into the debug menu
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentPhase = Phases.Phase_2;
        }
        //To put into the debug menu
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentPhase = Phases.Phase_1;
        } //To put into the debug menu
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentPhase = Phases.Phase_3;
        }



        //To put into the debug menu
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            ScenarioManager.Instance.UpdateScenario(1);
        }
        //To put into the debug menu
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            ScenarioManager.Instance.UpdateScenario(2);
        }

        //To put into the debug menu
        if (Keyboard.current.f6Key.wasPressedThisFrame)
        {
            SceneLoader.Instance.LoadByDevMenu("Office");
        }
        //To put into the debug menu
        if (Keyboard.current.f7Key.wasPressedThisFrame)
        {
            SceneLoader.Instance.LoadByDevMenu("HomeInvasion");
        }
        //To put into the debug menu
        if (Keyboard.current.f8Key.wasPressedThisFrame)
        {
            SceneLoader.Instance.LoadByDevMenu("RisingWater");
        }


        //To put into the debug menu
        if (Keyboard.current.numpadPlusKey.wasPressedThisFrame)
        {
            OrderController.Instance.ResolvePuzzle();
        }


        //To put into the debug menu
        if (Keyboard.current.f10Key.wasPressedThisFrame)
        {
            TutoManager.Instance.Skip();
        }
        //To put into the debug menu
        if (Keyboard.current.f11Key.wasPressedThisFrame)
        {
            Reset();
        }
        //To put into the debug menu
        if (Keyboard.current.f12Key.wasPressedThisFrame)
        {
            Restart();
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


    public void Restart()
    {
        Reset();
        SceneLoader.Instance.Unload("Persistent");
        SceneLoader.Instance.Unload(SceneLoader.Instance.GetCurrentScene().name);
        //SceneLoader.Instance.LoadNewScene("Persistent");
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
                HeadPhoneManager.Instance.TakeOff();

                Projection.Instance.SetTransitionValue(50);
                Projection.Instance.enableTransition = false;
                break;

            case 1:
                Projection.Instance.enableTransition = true;
                Projection.Instance.SetTransitionValue(50);
                ScenarioManager.Instance.UpdateScenario(1);
                TimeSettings.Instance.Initialize();
                UpdateController();
                break;

            case 2:
                if (!Projection.Instance.onEditor)
                {
                    Projection.Instance.SetTransitionValue(0);
                }
                MasterManager.Instance.isInImaginary = true;
                UpdateController();
                WordManager.Instance.PullWord();

                Projection.Instance.enableTransition = false;
                HeadPhoneManager.Instance.headPhone.GetComponent<Rigidbody>().isKinematic = true;                
                break;

            case 3:
                Projection.Instance.enableTransition = true;
                Projection.Instance.SetTransitionValue(50);
                isTutoEnded = true;
                //isInImaginary = false;
                //Projection.Instance.revealScene = true;

                WordManager.Instance.PullWord();
                UIManager.Instance.UpdateUnitManager(4);
                HeadPhoneManager.Instance.OnPhaseChange(i);
                break;

            case 4:
                HeadPhoneManager.Instance.TakeOff();

                //ScenarioManager.Instance.UpdateScenario(1);
                Reset();
                break;
        }

        MusicManager.Instance.CheckMusic();
    }

    public void EnvironmentIsReveal()
    {
        envIsReveal = true;
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

        envIsReveal = false;

    }
    public void PlayDialogues()
    {
        if (!references.mainAudioSource.isPlaying && currentPhase == Phases.Phase_1)
        {
            references.mainAudioSource.PlayNewClipOnce(ScenarioManager.Instance.currentScenarioData.dialogues);
            this.CallWithDelay(WordManager.Instance.PullWord, ScenarioManager.Instance.currentScenarioData.timeAfterDialogueBegins);

            TimeSettings.Instance.StartGlobalTimer();
            if(UIManager.Instance != null)
            {
                UIManager.Instance.InComingCall(false);
            }

            
        }
    }

    public void ConcludCall()
    {
        references.mainAudioSource.PlayNewClipOnce(ScenarioManager.Instance.currentScenarioData.conclusion);
        this.CallWithDelay(CallEnded, 13f);
    }

    public void CallEnded()
    {
        isEnded = true;
        UIManager.Instance.OutComingCall(true);
    }

    public void ToggleAcoup(){
        aCoup = !aCoup;
    }

    public void SetCameraYOffset(float value){
        references.xRRig.cameraYOffset = value;
    }
    public void AddCameraYOffset(float value){
        references.xRRig.cameraYOffset += value;
    }
    public void RemoveCameraYOffset(float value){
        references.xRRig.cameraYOffset -= value;
    }
    public void AddCameraYOffset(TMP_Text text){
        float value = float.Parse(text.text);
        references.xRRig.cameraYOffset += value;
    }
    public void RemoveCameraYOffset(TMP_Text text){
        float value = float.Parse(text.text);
        references.xRRig.cameraYOffset -= value;
    }


}

[System.Serializable]
public class References
{
    [Header("XR")]
    public XRInteractionManager xRInteractionManager;
    public XRRig xRRig;
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
