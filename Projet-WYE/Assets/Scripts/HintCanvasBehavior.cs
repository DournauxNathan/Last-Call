using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class HintCanvasBehavior : MonoBehaviour
{
    public TMP_Text _text;
    public AudioSource _audioSource;
    public HintInWorld _hintInWorldData;

    void Update()
    {
        if(MasterManager.Instance !=null) transform.rotation = MasterManager.Instance.references.mainCamera.transform.rotation;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayNewClipOnce(clip);
    }
}
