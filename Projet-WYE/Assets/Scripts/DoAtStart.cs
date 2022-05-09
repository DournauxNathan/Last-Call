using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoAtStart : MonoBehaviour
{
    public float timer;
    public bool isCountingDown;

    private void Start()
    {
        timer = ScenarioManager.Instance.currentScenarioData.timerInPhase2;
        
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            yield return null;

            if (timer <= 0)
            {
                timer = 0;

                MasterManager.Instance.isEnded = true;
                Debug.Log("Call is over");
                    
                StopAllCoroutines();
            }
        }
    }

}
