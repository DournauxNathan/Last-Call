using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundInteraction : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("SoundInteraction: AudioSource not found on " + gameObject.name);
        }
    }

    public void CallSound(AudioClip clip)
    {
        if(clip != null)
        {
        audioSource.PlayNewClipOnce(clip);
        }
        else{
            Debug.LogError("SoundInteraction: AudioClip not found on " + gameObject.name);
        }
    }

}
