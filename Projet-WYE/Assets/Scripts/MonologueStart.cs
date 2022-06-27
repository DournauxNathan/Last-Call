using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MonologueStart : MonoBehaviour
{
    private AudioSource _audiosource;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        _audiosource = GetComponent<AudioSource>();

        yield return new WaitForSeconds(1f);

        if(ScenarioManager.Instance.currentScenario != Scenario.None)
        {
            //_audiosource.PlayNewClipOnce(ChoseMonologue());
        }
        else
        {
            Debug.LogError("no scenario loaded"+this.gameObject);
        }
    }

    private AudioClip ChoseMonologue()
    {
        if (ScenarioManager.Instance.endingValue == 0)
        {
            Debug.Log(ScenarioManager.Instance.currentScenarioData.monologue[0].name);

            return ScenarioManager.Instance.currentScenarioData.monologue[0];
        }
        else if(ScenarioManager.Instance.endingValue < 0)
        {
            return ScenarioManager.Instance.currentScenarioData.monologue[1];
        }
        else if(ScenarioManager.Instance.endingValue > 0)
        {
            return ScenarioManager.Instance.currentScenarioData.monologue[2];
        }
        else
        {
            Debug.LogError("Erreror value is not supported"+ this.gameObject);
            return null;
        }
    }
}
