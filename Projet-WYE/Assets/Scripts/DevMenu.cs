using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DevMenu : MonoBehaviour
{
    [SerializeField] private Transform mainMenu;
    [SerializeField] private bool isInDevMode = true;
    [SerializeField] private bool isEnable = false;
    [SerializeField] private bool isConfirming = false;
    [SerializeField] private bool hasCreatedJson = false;
    [SerializeField] private PlaytestData playtestData;
    [SerializeField] private TMP_InputField _InputField;
    [SerializeField] private Transform tester;
    [SerializeField] private Transform confirm;
    [SerializeField] private Transform report;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnable)
        {
            mainMenu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMenu();
        SendJson();
        GoToAppartment();
        GoToOffice();
    }

    private void ToggleMenu()
    {
        if (Keyboard.current[Key.F3].wasPressedThisFrame && isInDevMode)
        {
            if (isEnable)
            {
                mainMenu.gameObject.SetActive(false);
                isEnable = false;
            }
            else
            {
                mainMenu.gameObject.SetActive(true);
                _InputField.text = playtestData.betaTesteurs.player;
                Invoke("SelectField", 0.1f);// test
                isEnable = true;
            }
        }
    }

    private void SelectField()
    {
        _InputField.ActivateInputField();

    }

    public void ChangeTesterName()
    {
        playtestData.betaTesteurs.player = _InputField.text;
    }

    private void SendJson()
    {
        if (Keyboard.current[Key.F4].wasPressedThisFrame && isEnable && isInDevMode && isConfirming)
        {
            confirm.gameObject.SetActive(false);
            report.gameObject.SetActive(true);
            //create Json
            playtestData.SaveIntoJson();


            hasCreatedJson = true;
            isConfirming = false;
        }
        else if(Keyboard.current[Key.F4].wasPressedThisFrame && isEnable && isInDevMode && !hasCreatedJson)
        {
            isConfirming = true;
            tester.gameObject.SetActive(false);
            confirm.gameObject.SetActive(true);
        }

        else if (Keyboard.current[Key.Escape].wasPressedThisFrame && !isConfirming)
        {
            report.gameObject.SetActive(false);
            tester.gameObject.SetActive(true);
        }
        
        else if(Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            confirm.gameObject.SetActive(false);
            tester.gameObject.SetActive(true);
            isConfirming = false;
        }
    }

    private void GoToAppartment()
    {
        if (Keyboard.current[Key.F1].wasPressedThisFrame && isEnable)
        {
            SceneLoader.Instance.LoadNewScene("Appartment");
        }
    }

    private void GoToOffice()
    {
        if (Keyboard.current[Key.F2].wasPressedThisFrame && isEnable)
        {
            SceneLoader.Instance.LoadNewScene("Office");
        }
    }
}
