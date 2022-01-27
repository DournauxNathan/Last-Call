using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Calls : MonoBehaviour
{

    public AudioClip ringtone;
    public AudioClip voicesLine;

    public bool triggerCall = false;
    private bool hasAnsered = false;
    public bool hasDetatch = false;
    [SerializeField]private float timeToAnswer = 1f;
    private bool hasRingtone = false;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        while (triggerCall)
        {
            triggerCall = false;
            hasRingtone = true;
            hasAnsered = false;
            timeToAnswer = 1f;
            audioSource.clip = ringtone;
            audioSource.spatialBlend = 1f;
            audioSource.loop = true;
            audioSource.Play();

        }

        while (hasAnsered && hasRingtone)
        {
            hasAnsered = false;
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.clip = voicesLine;
            //audioSource.spatialBlend = 0.2f;
            audioSource.Play();
        }

        if (hasDetatch && timeToAnswer>0)
        {
            timeToAnswer -= Time.deltaTime;
        }
        else if (timeToAnswer <= 0 && hasDetatch)
        {
            hasDetatch = false;
            hasAnsered = true;

            audioSource.loop = false;
            audioSource.Stop();
        }

        
    }

    public void HasDetatch()
    {
        hasDetatch = true;
    }

}
