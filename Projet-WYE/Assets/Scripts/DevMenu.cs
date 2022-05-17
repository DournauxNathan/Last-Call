using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DevMenu : Singleton<DevMenu>
{

    [SerializeField] private Transform f5Menu;

    [SerializeField] private bool isInDevMode = true;
    [SerializeField] private bool isEnable = false;

    [Header("Panel - Phases")]
    [SerializeField] private TMP_Text currentPhase;

    [Header("Panel - Player Data")]
    [SerializeField] private PlaytestData playtestData;
    [SerializeField] private TMP_InputField _InputField;

    // Start is called before the first frame update
    void Start()
    {
        if (!isEnable)
        {
            f5Menu.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[Key.F5].wasPressedThisFrame && isInDevMode)
        {
            OpenF5Menu();
        }
        UpdatePhase(MasterManager.Instance.currentPhase);
    }

    public void OpenF5Menu()
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

    private void SelectField()
    {
        _InputField.ActivateInputField();

    }

    public void UpdatePhase(Phases _phase)
    {
        switch (_phase)
        {
            case Phases.Phase_0:
                currentPhase.text = "Phase (current: 0)";
                break;
            case Phases.Phase_1:
                currentPhase.text = "Phase (current: 1)";
                break;
            case Phases.Phase_2:
                currentPhase.text = "Phase (current: 2)";
                break;
            case Phases.Phase_3:
                currentPhase.text = "Phase (current: 3)";
                break;
            case Phases.Phase_4:
                currentPhase.text = "Phase (current: 4)";
                break;
        }

        Debug.Log(currentPhase.text);
    }

    public void ChangeTesterName()
    {
        playtestData.betaTesteurs.player = _InputField.text;
    }

    public void SendJson()
    {
        playtestData.SaveIntoJson();
    }

}
