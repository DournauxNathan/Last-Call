using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DevMenu : MonoBehaviour
{
    [SerializeField] private Transform f3Menu;
    [SerializeField] private Transform f5Menu;
    [SerializeField] private Transform XRf5Menu;
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
            f3Menu.gameObject.SetActive(false);
            f5Menu.gameObject.SetActive(false);
            XRf5Menu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMenu();
        SendJson();
    }

    private void ToggleMenu()
    {
        if (Keyboard.current[Key.F3].wasPressedThisFrame && isInDevMode)
        {
            if (isEnable)
            {
                f3Menu.gameObject.SetActive(false);
                isEnable = false;
            }
            else
            {
                f3Menu.gameObject.SetActive(true);
                _InputField.text = playtestData.betaTesteurs.player;
                Invoke("SelectField", 0.1f);// test
                isEnable = true;
            }
        }

        if (Keyboard.current[Key.F5].wasPressedThisFrame && isInDevMode)
        {
            if (isEnable)
            {
                f5Menu.gameObject.SetActive(false);
                isEnable = false;
            }
            else
            {
                f5Menu.gameObject.SetActive(true);
                isEnable = true;
            }
        }
    }

    public void OpenF5Menu()
    {
        if (isEnable)
        {
            XRf5Menu.gameObject.SetActive(false);
            isEnable = false;
        }
        else
        {
            XRf5Menu.gameObject.SetActive(true);
            isEnable = true;
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

}
