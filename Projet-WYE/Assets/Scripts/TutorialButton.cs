using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : InstantiableButton
{
    [Header("Tutorial Proporties")]
    public string tutorialText;
    public List<GameObject> tutoButtons;
    public AudioClip audioToPlay;
    private AudioSource audioSource;
    private bool isDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        text.text = tutorialText;

        audioSource = MasterManager.Instance.mainAudioSource;
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
            Destroy(gameObject);
        }
    }


    public void OnClick()
    {
        foreach (var button in tutoButtons)
        {
            //Destroy(button);
            button.gameObject.SetActive(false);
        }

        audioSource.Stop();
        audioSource.clip = audioToPlay;
        audioSource.Play();
    }
}
