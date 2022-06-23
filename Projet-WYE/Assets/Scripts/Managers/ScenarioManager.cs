using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum Scenario
{
    None = 0,
    HomeInvasion = 1,
    RisingWater = 2,
    TrappedMan = 5
}

public class ScenarioManager : Singleton<ScenarioManager>
{
    public Scenario currentScenario;
    public List<ScenarioData> scenarios;
    public ScenarioData currentScenarioData;

    public bool isScenarioLoaded = false;
    [Range(-20, 20)]
    public int endingValue = 0;
    public bool skipCurrentScenario;

    public void SetCurrentScenario(int index/*Scenario nextScenario*/)
    {
        //For Test and Debug
        switch (index)
        {
            case 1:
                currentScenario = Scenario.HomeInvasion;
                break;
            case 2:
                currentScenario = Scenario.RisingWater;
                break;/*
            case 3:
                currentScenario = Scenario.RisingWater;
                break;*/
        }

        //Pre Update the next scenario data
        //currentScenario = nextScenario;
    }

    public void LoadScenario()
    {
        HandController.Instance.showController = false;

        switch (currentScenario)
        {
            case Scenario.None:
                break;/*
            case Scenario.TrappedMan:
                WordManager.Instance.answers.AddRange(scenarios[0].answers);
                WordManager.Instance.questions.AddRange(scenarios[0].questions);
                currentScenarioData = scenarios[0];
                OrderController.Instance.puzzleNumber = currentScenarioData.numberOfPuzzle;
                break;*/

            case Scenario.HomeInvasion:
                WordManager.Instance.answers.AddRange(scenarios[0].answers);
                WordManager.Instance.questions.AddRange(scenarios[0].questions);
                currentScenarioData = scenarios[0];
                OrderController.Instance.puzzleNumber = currentScenarioData.numberOfPuzzle;
                break;

            case Scenario.RisingWater:
                WordManager.Instance.answers.AddRange(scenarios[1].answers);
                WordManager.Instance.questions.AddRange(scenarios[1].questions);
                currentScenarioData = scenarios[1];
                OrderController.Instance.puzzleNumber = currentScenarioData.numberOfPuzzle;
                break;
        }


        isScenarioLoaded = true;
        //Debug.Log(currentScenario.ToString() + " has been loaded");
        isScenarioLoaded = false;
    }

    public void StartScenario()
    {
        this.CallWithDelay(PreWriteForm, ScenarioManager.Instance.currentScenarioData.timeAfterDialogueBegins);

    }

    bool doOnce = true;
    public void PreWriteForm()
    {
        if (doOnce)
        {
            doOnce = !doOnce;
            UIManager.Instance.UpdateForm(FormData.name, currentScenarioData.callerInformations.name);
        }
    }

    public void UpdateEndingsValue(int modifier)
    {
        endingValue += modifier;
    }

    public Scenario GetCurrentScenario()
    {
        return currentScenario;
    }

    public int currentIndexScenario = 0;
    public void UpdateScenario(int i)
    {
        //Debug.Log(currentIndexScenario);
        currentIndexScenario++;
/*
        if (!skipCurrentScenario)
        {
            skipCurrentScenario = true;
            currentIndexScenario++;
        }*/

        switch (currentIndexScenario)
        {
            case 1:
                currentScenario = Scenario.HomeInvasion;
                //currentScenario = Scenario.TrappedMan;
                LoadScenario();
                break;
            case 2:
                currentScenario = Scenario.RisingWater;
                LoadScenario();
                break;/*
            case 3:
                //currentScenario = Scenario.RisingWater;
                LoadScenario();
                break;*/
        }

        //Debug.Log(currentScenario);

    }
}

[System.Serializable]
public class Form
{
    public TMP_Text nameField;
    public TMP_Text ageField;
    public TMP_Text adressField;
    public TMP_Text situationField;
    public TMP_Text unitField;
    public Image stamp;

    public bool isComplete;
}

public enum FormData
{
    name,
    age,
    adress,
    situation,  
    unit,
}