using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField]private bool isOn = false;
    [Header("Events")]
    public UnityEvent OnPauseEnter;
    public UnityEvent OnPauseExit;


    void Start()
    {
        UnPause();
        BackToMainMenu();
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

    public void GoBackToMainAppart() {
        UnPause();
        SceneLoader.Instance.LoadNewScene("Appartment");
        
    }

    public void DisplayPathText()
    {
        _text.text = "File Saved to : " + FileHandler.GetPath("SaveLastCall.json");
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

}
