using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scenario
{
    TrappedMan,
    HomeInvasion,
    DomesticAbuse,
    RisingWater
}

public class ScenarioManager : Singleton<ScenarioManager>
{
    public Scenario currentScenario;

    public bool isScenarioLoaded = false;
    [Range(-10, 10)]
    public float endingValue = 0f;

    [Header("Questions - Protocoles ")]
    public List<QuestionFormat> protocol;

    [Header("Questions - Description")]
    public List<QuestionFormat> trappedMan;
    public List<QuestionFormat> homeInvasion;
    public List<QuestionFormat> domesticAbuse;
    public List<QuestionFormat> risingWater;

    [Header("Orders")]
    public List<OrderFormat> o_trappedMan;
    public List<OrderFormat> o_homeInvasion;
    public List<OrderFormat> o_domesticAbuse;

    [Header("Answers - Protocoles")]
    public List<ProtocolFormat> p_trappedMan;
    public List<ProtocolFormat> p_homeInvasion;
    public List<ProtocolFormat> p_domesticAbuse;

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
            case 4:
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
                    UIManager.Instance.descriptionQuestion.AddRange(trappedMan);
                    break;
                case Scenario.HomeInvasion:
                    UIManager.Instance.descriptionQuestion.AddRange(homeInvasion);
                    break;
                case Scenario.DomesticAbuse:
                    UIManager.Instance.descriptionQuestion.AddRange(domesticAbuse);
                    break;
        }
        //LoadProtocolAnswer();Debug.Log("Load: " + currentScenario.ToString()); // 
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

    private void LoadProtocolAnswer()
    {
        switch (currentScenario)
        {
            case Scenario.TrappedMan:
                for (int i = 0; i < protocol.Count; i++)
                {
                    protocol[i].listAnswers[0] = p_trappedMan[i].protocolAnswer;
                    protocol[i].voiceLineAnswer[0] = p_trappedMan[i].protocolAnswerAudio;
                }

                break;
            case Scenario.HomeInvasion:
                for (int i = 0; i < protocol.Count; i++)
                {
                    protocol[i].listAnswers[0] = p_homeInvasion[i].protocolAnswer;
                    protocol[i].voiceLineAnswer[0] = p_homeInvasion[i].protocolAnswerAudio;
                }
                break;
            case Scenario.DomesticAbuse:
                for (int i = 0; i < protocol.Count; i++)
                {
                    protocol[i].listAnswers[0] = p_domesticAbuse[i].protocolAnswer;
                    protocol[i].voiceLineAnswer[0] = p_domesticAbuse[i].protocolAnswerAudio;
                }
                break;
            case Scenario.RisingWater:
                break;
        }
    }
}
