using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtentions 
{
   public static void PlayNewClipOnce(this AudioSource audioSource, AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
