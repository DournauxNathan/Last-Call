using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class UiPauseManager : Singleton<UiPauseManager>
{
    [Header("Debug")]
    [SerializeField] private GameObject defaultSelected;
    [SerializeField] private Transform pauseBase;
    [SerializeField] private List<Transform> SubMenus;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject current;
    [SerializeField] private bool isOn = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private bool DebugOn = false;

    public List<AudioClip> audioClips;
    [Header("Events")]
    public UnityEvent OnPauseEnter;
    public UnityEvent OnPauseExit;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UnPause();

        if (DebugOn)
        {
            isOn = true; BackToMainMenu();
        }
        _text.text = "";
    }
    private void SetUp()
    {
        EventSystem.current.SetSelectedGameObject(defaultSelected);
        _text.text = "";
    }

    private void Update()
    {
        if (current == null  && isOn|| EventSystem.current.currentSelectedGameObject != null && current != EventSystem.current.currentSelectedGameObject && isOn)
        {
            current = EventSystem.current.currentSelectedGameObject;
            audioSource.clip = audioClips[0];
            audioSource.Play();

        }

        //security
        if (EventSystem.current.currentSelectedGameObject == null && isOn)
        {
            EventSystem.current.SetSelectedGameObject(current);
        }

    }


    public void UnPause()
    {
        //Disable everything
        pauseBase.gameObject.SetActive(false);
        foreach (var sub in SubMenus)
        {
            sub.gameObject.SetActive(false);
        }
        isOn = false;
        audioSource.PlayNewClipOnce(audioClips[3]);
        OnPauseExit.Invoke();
    }

    public void PauseDisplay()
    {
        if (CheckPauseIsActive())
        {
            UnPause();
            isOn = false;
        }
        else
        {
            DisplayTarget(pauseBase.gameObject);
            SetUp();
            isOn = true;
            audioSource.PlayNewClipOnce(audioClips[2]);
            OnPauseEnter.Invoke();
        }
    }

    public void BackToMainMenu()
    {
        pauseBase.gameObject.SetActive(true);
        foreach (var sub in SubMenus)
        {
            sub.gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }

    public void DisplayTarget(GameObject target)
    {
        pauseBase.gameObject.SetActive(false);

        foreach (var sub in SubMenus)
        {
            sub.gameObject.SetActive(false);
        }

        target.SetActive(true);

        if (target.name == "Option")
        {
            EventSystem.current.SetSelectedGameObject(target.transform.GetChild(2).GetChild(0).GetChild(0).gameObject);
        }
        if (target.name == "SaveConfirm")
        {
            EventSystem.current.SetSelectedGameObject(target.transform.GetChild(2).gameObject);
        }
        if (target.name == "MenuConfirm")
        {
            EventSystem.current.SetSelectedGameObject(target.transform.GetChild(2).gameObject);
        }

    }

    public void SelectTraget(GameObject target)
    {
        EventSystem.current.SetSelectedGameObject(target);
    }

    public void GoBackToMainAppart()
    {
        MasterManager.Instance.ChangeSceneByName(0, "Menu");
        UnPause();
    }

    public void DisplayPathText()
    {
        _text.text = "File Saved to : " + FileHandler.GetPath("SaveLastCall.json"); //Hardcode 
    }
    
    private bool CheckPauseIsActive()
    {
        if (pauseBase.gameObject.activeSelf)
        {
            return true;
        }
        else
        {
            foreach (var sub in SubMenus)
            {
                if (sub.gameObject.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void DisablingMainMenu() 
    {
        FindObjectOfType<UIMenuManager>().enabled = false;
    }

    public void EnablingMainMenu()
    {
        FindObjectOfType<UIMenuManager>(true).enabled = true;

    }

    public void ValidateSound()
    {
        StartCoroutine(DisableAudioOne(audioClips[1]));
        audioSource2.PlayNewClipOnce(audioClips[1]);
    }

    public void Back()
    {
        StartCoroutine(DisableAudioOne(audioClips[4]));
        audioSource2.PlayNewClipOnce(audioClips[4]);
    }
    public void Triche()
    {
        audioSource.PlayNewClipOnce(audioClips[3]);
    }

    private IEnumerator DisableAudioOne(AudioClip audioClip)
    {
        var time = audioClip.length;
        audioSource.mute = true;
        yield return new WaitForSeconds(time);
        audioSource.mute = false;

    }

    public void AddCameraYOffset()
    {
        MasterManager.Instance.AddCameraYOffset(MasterManager.Instance.offsetForCamera);
    }
    public void RemoveCameraYOffset()
    {
        MasterManager.Instance.AddCameraYOffset(-MasterManager.Instance.offsetForCamera);
    }

}
