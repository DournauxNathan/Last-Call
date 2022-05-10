using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSettings : Singleton<TimeSettings>
{
    public float timeBeforeCall;
    public float globalTimer = 0f;

    public bool isRunning;

    public bool runAtStart;

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

    public IEnumerator IncreaseTime()
    {
        while (globalTimer >= 0)
        {
            globalTimer += Time.deltaTime;

            if (globalTimer >= ScenarioManager.Instance.currentScenarioData.ageBegin
                && globalTimer < ScenarioManager.Instance.currentScenarioData.adressBegin)
            {
                foreach (var item in AnswerManager.Instance.age)
                {
                    item.SetActive(true);
                }
            }
            if (globalTimer >= ScenarioManager.Instance.currentScenarioData.adressBegin
                && globalTimer < ScenarioManager.Instance.currentScenarioData.situationBegin)
            {
                foreach (var item in AnswerManager.Instance.adress)
                {
                    item.SetActive(true);
                }
            }
            if (globalTimer > ScenarioManager.Instance.currentScenarioData.situationBegin)
            {
                foreach (var item in AnswerManager.Instance.situations)
                {
                    for (int i = 0; i < item.canvas.Count; i++)
                    {
                        item.canvas[i].SetActive(true);
                    }
                }
            }

            yield return null;
        }
    }

    public void SetTime(float value)
    {
        timeBeforeCall = value;

        if (UIManager.Instance != null)
            UIManager.Instance.InComingCall(true);
    }


}
