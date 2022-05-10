using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    private bool isPlaying = false;

    public List<string> tags;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        if(tags.Count == 0){
            Debug.LogError("No tags defined for sound trigger");
            return;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.outputAudioMixerGroup = MasterManager.Instance.references.sfx;
        DisablingAudioSource();
    }

    IEnumerator DisablingAudioSource()
    {
        audioSource.enabled = false;
        yield return new WaitForSeconds(3f);
        audioSource.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(!isPlaying && tags.Contains(other.tag)){
            isPlaying = true;
            audioSource.PlayNewClipOnce(clip);

        }

    }

    private void OnTriggerExit(Collider other) {
        isPlaying = false;
    }

}
