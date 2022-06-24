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

    public void StartGlobalTimer()
    {
        StartCoroutine(IncreaseTime());
    }

    public bool unPause;

    public IEnumerator IncreaseTime()
    {
        while (globalTimer >= 0)
        {
            if (MasterManager.Instance.references.mainAudioSource.isPlaying || unPause)
            {
                globalTimer += Time.deltaTime;
            }

            if (globalTimer >= ScenarioManager.Instance.currentScenarioData.ageBegin
                && globalTimer < ScenarioManager.Instance.currentScenarioData.adressBegin
                && !AnswerManager.Instance.ageIsAnswered)
            {
                //Debug.Log("Pause audio A");
                this.CallWithDelay(() => MasterManager.Instance.references.mainAudioSource.Pause(), 3.2f);
                foreach (var item in AnswerManager.Instance.age)
                {
                    this.CallWithDelay(() => item.SetActive(true), 3.4f);
                }
                MasterManager.Instance.references.mainAudioSource.UnPause();
                unPause = true;
            }
            if (globalTimer >= ScenarioManager.Instance.currentScenarioData.adressBegin
                && globalTimer < ScenarioManager.Instance.currentScenarioData.situationBegin
                && !AnswerManager.Instance.adressIsAnswer && AnswerManager.Instance.ageIsAnswered)
            {
                //Debug.Log("Pause audio B");
                if (doOnceB)
                {
                    this.CallWithDelay(() => MasterManager.Instance.references.mainAudioSource.Pause(), 5.4f);
                    doOnceB = !doOnceB;
                    foreach (var item in AnswerManager.Instance.adress)
                    {
                        this.CallWithDelay(() => item.SetActive(true), 5.6f);
                    }
                    MasterManager.Instance.references.mainAudioSource.UnPause(); unPause = true;
                }
            }
            if (globalTimer > ScenarioManager.Instance.currentScenarioData.situationBegin
                && !AnswerManager.Instance.situationIsAnswer && AnswerManager.Instance.adressIsAnswer && AnswerManager.Instance.ageIsAnswered)
            {
                //Debug.Log("Pause audio C");

                if (doOnce)
                {
                    doOnce = !doOnce;

                    foreach (var item in AnswerManager.Instance.situations)
                    {
                        this.CallWithDelay(() => {
                            for (int i = 0; i < item.canvas.Count; i++)
                            {
                                item.canvas[i].SetActive(true);
                                //MasterManager.Instance.references.mainAudioSource.UnPause();
                            }
                        }, 40f);
                    }
                    MasterManager.Instance.references.mainAudioSource.UnPause(); unPause = true;
                    //this.CallWithDelay(() => MasterManager.Instance.references.mainAudioSource.Pause(), 2f);
                }

            }
            if (globalTimer >= 100f)
            {
                StopCoroutine(IncreaseTime());
                TutoManager.Instance.UpdateIndication(2);
            }

            yield return null;

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
