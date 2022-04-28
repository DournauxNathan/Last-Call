﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] private List<MySlider> sliders;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voicesSlider;

    public List<AudioClip> audioClips;
    [SerializeField]private AudioSource audioSource;

    const string MIXER_MASTER = "MasterVolume";
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";
    const string MIXER_VOICES = "VoicesVolume";

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        voicesSlider.onValueChanged.AddListener(SetVoicesVolume);

        foreach (var MySlider in sliders)
        {
            MySlider.onValueChangedWithOldValue.AddListener(VolumeSound);
        }

    }
    private void Start()
    {
        //audioSource = this.transform.parent.parent.parent.parent.GetComponentInParent<AudioSource>();
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER,/*Mathf.Log10(*/value/*)*20*/);
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC,/*Mathf.Log10(*/value/*)*20*/);
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX,/*Mathf.Log10(*/value/*)*20*/);
    }
    void SetVoicesVolume(float value)
    {
        mixer.SetFloat(MIXER_VOICES,/*Mathf.Log10(*/value/*)*20*/);
    }

    public void VolumeSound(float value)
    {
        var old = value;
        if (old>value)
        {
            audioSource.PlayNewClipOnce(audioClips[1]);
        }
        else
        {
            audioSource.PlayNewClipOnce(audioClips[0]);
        }
    }

    public void VolumeSound(float oldValue, float value)
    {
        if (oldValue > value)
        {
            audioSource.PlayNewClipOnce(audioClips[1]);
        }
        else
        {
            audioSource.PlayNewClipOnce(audioClips[0]);
        }
    }



}
