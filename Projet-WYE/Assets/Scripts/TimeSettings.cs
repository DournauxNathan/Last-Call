using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSettings : Singleton<TimeSettings>
{
    public float timeBeforeCall;


    public bool isRunning;
    
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

                StopAllCoroutines();
            }   
        }  
    }

    public void SetTime(float value)
    {
        timeBeforeCall = value;

        if (UIManager.Instance != null)
            UIManager.Instance.InComingCall(true);
    }


}
