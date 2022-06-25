using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSettings : Singleton<TimeSettings>
{
    public float timeBeforeCall;
    public float globalTimer = 0f;

    public bool isRunning;

    public bool runAtStart;

    bool doOnce = true;
    bool doOnceB = true;
    private void Start()
    {
        if (runAtStart)
        {
            timeBeforeCall = ScenarioManager.Instance.currentScenarioData.callSettings.timeBeforeCall;
            StartCoroutine(DecreaseTime(ScenarioManager.Instance.currentScenarioData.callSettings.timeBeforeCall));
        }
    }

    public void Initialize()
    {
        timeBeforeCall = ScenarioManager.Instance.currentScenarioData.callSettings.timeBeforeCall;
        StartCoroutine(DecreaseTime(ScenarioManager.Instance.currentScenarioData.callSettings.timeBeforeCall));
    }

    public IEnumerator DecreaseTime(float value)
    {
        while (value > 0)
        {
            isRunning = true;

            value -= Time.deltaTime;

            yield return null;

            if (value <= 0)
            {
                value = 0;
                SetTime(value);
                StopCoroutine("DecreaseTime");
            }   
        }  
    }

    public bool timerBegin;

    public void StartGlobalTimer()
    {
        StartCoroutine(IncreaseTime());
    }

    public bool isAudioPause;

    public IEnumerator IncreaseTime()
    {
        timerBegin = true;
        while (globalTimer >= 0 && !isAudioPause)
        {
            globalTimer += Time.deltaTime;

            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (!isAudioPause && timerBegin)
        {
            UpdateAnswer();
        }
    }

    public void UpdateAnswer()
    {
        if (globalTimer >= ScenarioManager.Instance.currentScenarioData.ageBegin && globalTimer < ScenarioManager.Instance.currentScenarioData.adressBegin && !AnswerManager.Instance.ageIsAnswered)
        {
            this.CallWithDelay(() => MasterManager.Instance.references.mainAudioSource.Pause(), 3.2f);
            isAudioPause = true;

            foreach (var item in AnswerManager.Instance.age)
            {
                this.CallWithDelay(() => item.SetActive(true), 3.4f);
            }

            MasterManager.Instance.references.mainAudioSource.UnPause();
        }
        if (globalTimer >= ScenarioManager.Instance.currentScenarioData.adressBegin && globalTimer < ScenarioManager.Instance.currentScenarioData.situationBegin && !AnswerManager.Instance.adressIsAnswer && AnswerManager.Instance.ageIsAnswered)
        {
            if (doOnceB)
            {
                doOnceB = !doOnceB;

                this.CallWithDelay(() => MasterManager.Instance.references.mainAudioSource.Pause(), 5.4f);
                isAudioPause = true;
                foreach (var item in AnswerManager.Instance.adress)
                {
                    this.CallWithDelay(() => item.SetActive(true), 5.6f);
                }

                MasterManager.Instance.references.mainAudioSource.UnPause();
            }
        }
        if (globalTimer >= ScenarioManager.Instance.currentScenarioData.situationBegin && !AnswerManager.Instance.situationIsAnswer && AnswerManager.Instance.adressIsAnswer && AnswerManager.Instance.ageIsAnswered)
        {
            if (doOnce)
            {
                doOnce = !doOnce;
                isAudioPause = true;
                foreach (var item in AnswerManager.Instance.situations)
                {
                    this.CallWithDelay(() => {
                        for (int i = 0; i < item.canvas.Count; i++)
                        {
                            item.canvas[i].SetActive(true);
                            //MasterManager.Instance.references.mainAudioSource.UnPause();
                        }
                    }, 12f);
                }

                //MasterManager.Instance.references.mainAudioSource.UnPause();
            }

        }
        if (globalTimer >= 100f)
        {
            StopCoroutine(IncreaseTime());

            timerBegin = false;

            TutoManager.Instance.UpdateIndication(2);
        }
    }

    public void SetTime(float value)
    {
        timeBeforeCall = value;

        if (SceneLoader.Instance.GetCurrentScene().name == "Office" && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            UIManager.Instance.InComingCall(true);
        }
    }


}
