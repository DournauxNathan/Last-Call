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

    public AudioSource phone;

    // Start is called before the first frame update
    void Start()
    {
        phone.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        while (triggerCall)
        {
            triggerCall = false;
            phone.clip = ringtone;
            phone.spatialBlend = 1f;
            phone.loop = true;
            phone.Play();

        }

        while (hasAnsered)
        {
            hasAnsered = false;
            phone.loop = false;
            phone.Stop();
            phone.clip = voicesLine;
            phone.spatialBlend = 0.2f;
            phone.Play();
        }
    }
}
