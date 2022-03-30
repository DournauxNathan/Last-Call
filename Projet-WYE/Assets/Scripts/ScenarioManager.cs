using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scenario
{
    TrappedMan,
    HomeInvasion,
    RisingWater
}

public class ScenarioManager : Singleton<ScenarioManager>
{
    public Scenario currentScenario;
    public List<ScenrioData> scenarios;

    public bool isScenarioLoaded = false;
    [Range(-10, 10)]
    public float endingValue = 0f;

    [Header("Questions - Description")]
    public List<QuestionFormat> trappedMan;
    public List<QuestionFormat> homeInvasion;
    public List<QuestionFormat> domesticAbuse;
    public List<QuestionFormat> risingWater;


    public void SetCurrentScenario(int index/*Scenario nextScenario*/)
    {
        //For Test and Debug
        switch (index)
        {
            case 1:
                currentScenario = Scenario.TrappedMan;
                break;
            case 2:
                currentScenario = Scenario.HomeInvasion;
                break;
            case 3:
                currentScenario = Scenario.RisingWater;
                break;
        }

        //Pre Update the next scenario data
        //currentScenario = nextScenario;
    }

    public void LoadScenario()
    {
        switch (currentScenario)
        {
                case Scenario.TrappedMan:
                WordManager.Instance.answers.AddRange(scenarios[0].answers);
                    break;
                case Scenario.HomeInvasion:
                WordManager.Instance.answers.AddRange(scenarios[1].answers);
                break;
                case Scenario.RisingWater:
                WordManager.Instance.answers.AddRange(scenarios[2].answers);
                break;
        }

        //Debug.Log("Load: " + currentScenario.ToString());
        isScenarioLoaded = true;
    }

    public void UpdateEndingsValue(int modifier)
    {
        endingValue += (float)modifier;
    }

    public Scenario GetCurrentScenario()
    {
        return currentScenario;
    }

}

[System.Serializable]
public class ScenrioData
{
    public Scenario scenario;
    public List<Answer> answers;
}
