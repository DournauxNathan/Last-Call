using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialButton : InstantiableButton
{
    [Header("Tutorial Proporties")]
    public string tutorialText;
    public List<GameObject> tutoButtons;
    public AudioClip audioToPlay;
    private bool isDestroyed;
    private bool isPlaying;

    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        text.text = tutorialText;
    }

    // Update is called once per frame
    void Update()
    {
        if (simulateInput)
        {
            SetSimulateInput(false);
            OnClick();
        }

        if (MasterManager.Instance.isTutoEnded && !isDestroyed)
        {
            isDestroyed = true;
            gameObject.SetActive(false);

            foreach (var button in tutoButtons)
            {
                gameObject.transform.SetAsLastSibling();
            }
        }

        if (isPlaying && !audioSource.isPlaying)
        {
            MasterManager.Instance.isTutoEnded = true;
            
            foreach (var button in tutoButtons)
            {
                gameObject.SetActive(false);
                gameObject.transform.SetAsLastSibling();
            }
        }
    }

    public void OnClick()
    {
        isPlaying = true;

        audioSource.Stop();
        audioSource.clip = audioToPlay;
        audioSource.Play();

        foreach (var but in tutoButtons)
        {
            toggle.enabled = true;
            toggle.isOn = true;
            img.enabled = false;
        }
    }
}
