using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


public class HintManager : MonoBehaviour
{
    public List<Hint> hints;
    public float setTimer;
    public float timeBetweenHint;

    [SerializeField] private float timer;
    private bool playHint = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = MasterManager.Instance.mainAudioSource;
        SetTimer();
    }

    private void Update()
    {
        if (hints.Count != 0)
        {
            UpdateTimer();
        }
    }

    public void SetTimer()
    {
        timer = setTimer;
    }

    public void UpdateTimer()
    {
        if (timer > 0 &&MasterManager.Instance.isInImaginary && !playHint)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            StartCoroutine(PlayHint());
            playHint = true;
            timer = 0;
        }
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        playHint = false;
        timer = timeBetweenHint;
    }

    public IEnumerator PlayHint()
    {
        while (true)
        {
            switch (ScenarioManager.Instance.GetCurrentScenario())
            {
                case ScenarioManager.Scenario.TrappedMan:
                    if (playHint)
                    {
                        int minA = Random.Range(0, hints[0].hint_voiceLines.Count());
                        audioSource.PlayOneShot(hints[0].hint_voiceLines[minA], 1.0f);
                        playHint = false;
                    }
                    yield return null;
                    break;

                case ScenarioManager.Scenario.HomeInvasion:
                    if (playHint)
                    {
                        int minB = Random.Range(0, hints[1].hint_voiceLines.Count());
                        audioSource.PlayOneShot(hints[1].hint_voiceLines[minB], 1.0f);
                        playHint = false;
                    }
                    yield return null;
                    break;

                case ScenarioManager.Scenario.DomesticAbuse:
                    if (playHint)
                    {
                        int minC = Random.Range(0, hints[1].hint_voiceLines.Count());
                        audioSource.PlayOneShot(hints[1].hint_voiceLines[minC], 1.0f);
                        playHint = false;
                        Debug.Log("");
                    }
                    yield return null;
                    break;
            }

            ResetTimer();
        }
    }
}