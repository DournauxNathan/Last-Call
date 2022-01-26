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
    private bool isDestroyed;

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
            OnClick(audioSource);
        }

        if (MasterManager.Instance.isTutoEnded && !isDestroyed)
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }


    public void OnClick(AudioSource audio)
    {
        foreach (var button in tutoButtons)
        {
            //Destroy(button);
            button.gameObject.SetActive(false);
        }

        audio.Stop();
        audio.clip = audioToPlay;
        audio.Play();
    }
}
