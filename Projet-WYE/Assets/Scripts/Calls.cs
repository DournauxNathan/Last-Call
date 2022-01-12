using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Calls : MonoBehaviour
{

    public AudioClip ringtone;
    public AudioClip voicesLine;

    public bool triggerCall = false;
    public bool hasAnsered = false;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        while (triggerCall)
        {
            triggerCall = false;
            audioSource.clip = ringtone;
            audioSource.spatialBlend = 1f;
            audioSource.loop = true;
            audioSource.Play();

        }

        while (hasAnsered)
        {
            hasAnsered = false;
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.clip = voicesLine;
            audioSource.spatialBlend = 0.2f;
            audioSource.Play();
        }
    }
}
