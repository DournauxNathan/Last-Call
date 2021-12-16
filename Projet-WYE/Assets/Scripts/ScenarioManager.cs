using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : Singleton<ScenarioManager>
{
    public enum Scenario
    {
        TrappedMan,
        HomeInvasion,
        DomesticAbuse
    }

    public Scenario currentScenario;

    [Range(-10, 10)]
    [SerializeField] public float endingValue = 0f;

    [SerializeField] private List<QuestionFormat> trappedMan;
    [SerializeField] private List<QuestionFormat> homeInvasion;
    [SerializeField] private List<QuestionFormat> domesticAbuse;

    [SerializeField] public List<OrderFormat> o_trappedMan;
    [SerializeField] public List<OrderFormat> o_homeInvasion;
    [SerializeField] public List<OrderFormat> o_domesticAbuse;

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
                currentScenario = Scenario.DomesticAbuse;
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
                UIManager.Instance.questionData.AddRange(trappedMan);
                break;
            case Scenario.HomeInvasion:
                UIManager.Instance.questionData.AddRange(homeInvasion);
                break;
            case Scenario.DomesticAbuse:
                UIManager.Instance.questionData.AddRange(domesticAbuse);
                break;
        }
    }

    public void UpdateEndingsValue(int modifier)
    {
        endingValue += (float)modifier;
    }
}
