using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UnderWater : MonoBehaviour
{
    private AudioSource m_audioSource;
    public AudioClip underWaterSound;
    public GameObject PostProcessActuel;
    public GameObject PostProcessAquatique;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessAquatique.SetActive(false);
        if(TryGetComponent<AudioSource>(out AudioSource audioSource)){
            audioSource.clip = underWaterSound;
            m_audioSource = audioSource;
        }
        else{
            AudioSource _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = underWaterSound;
            m_audioSource = _audioSource;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (MasterManager.Instance != null && transform.position.y > MasterManager.Instance.references.mainCamera.position.y)
        {
            PostProcessActuel.SetActive(false);
            PostProcessAquatique.SetActive(true);
            m_audioSource.PlayNewClipOnce(underWaterSound);
        }
        else
        {
            PostProcessActuel.SetActive(true);
            PostProcessAquatique.SetActive(false);
        }
    }



}
