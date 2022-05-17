using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using System.Linq;

public class WordManager : Singleton<WordManager>
{
    public Transform getTransfrom;
    public Transform stockA, stockB, stockEntry;

    public List<Answer> answers;
    public List<Question> questions;
    public Reveal entry;

    public List<WordData> canvasWithWordData;
    public List<Reveal> canvasWithQuestionData;

    public bool isProtocolComplete;

    bool displayAdress = true;
    FormData answerType;

    public List<GameObject> questionsGo;

    public bool pullOrders;

    private void Update()
    {
        if (isProtocolComplete && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            UIManager.Instance.SetFormToComplete(true);
        }
    }

    public void PullWord()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            //Get all answer
            foreach (Answer answer in answers)
            {
                //Get the keywords in the answer
                for (int i = 0; i < answer.keywords.Length; i++)
                {
                    //Find any available Canvas Word 
                    var item = FindAvailableWordData();
                    //if true, Activate Canvas Word and Set his text with the current propo
                    item.Activate(transform, stockA, answer.keywords[i].isCorrectAnswer, answer.keywords[i].proposition, answer);

                    switch (item.GetAnswer().type)
                    {
                        case FormData.age:
                            AnswerManager.Instance.age.Add(item.gameObject);
                            break;
                        case FormData.adress:
                            AnswerManager.Instance.adress.Add(item.gameObject);
                            break;
                        case FormData.situation:
                            for (int y = 0; y < AnswerManager.Instance.situations.Count; y++)
                            {
                                if (AnswerManager.Instance.situations[y].id == item.GetAnswer().id)
                                {
                                    AnswerManager.Instance.situations[y].canvas.Add(item.gameObject);
                                }
                            }
                            break;
                    }
                }

                for (int i = 0; i < getTransfrom.childCount; i++)
                {
                    getTransfrom.GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_2 && MasterManager.Instance.isInImaginary)
        {
            foreach (Question question in questions)
            {
                //Get the keywords in the answer
                for (int i = 0; i < question.questions.Count; i++)
                {
                    //Find any available Canvas Word 
                    var item = FindAvailableReveal();
                    //if true, Activate Canvas Word and Set his text with the current propo
                    item.Activate(transform, stockB, question, question.questions[i].question, i);

                    questionsGo.Add(item.gameObject);
                }

                for (int i = 0; i < getTransfrom.childCount; i++)
                {
                    getTransfrom.GetChild(i).gameObject.SetActive(false);
                }

                if (displayAdress)
                {
                    displayAdress = !displayAdress;
                    var item = Entry();
                    item.Activate(transform, stockEntry, ScenarioManager.Instance.currentScenarioData.callerInformations.adress, ScenarioManager.Instance.currentScenarioData.callerInformations.adress.questions[0].question);

                }
            }
        }

        if (pullOrders || MasterManager.Instance.currentPhase == Phases.Phase_3 && !MasterManager.Instance.isInImaginary)
        {
            foreach (Order currentOrder in OrderController.Instance.ordersStrings)
            {
                //Find any available Canvas Word 
                var item = FindAvailableWordData();
                //if true, Activate Canvas Word and Set his text with the current propo
                item.Activate(transform, stockA, currentOrder.order);
            }
            
            Debug.Log("Pull Order");
        }
    }


    public WordData FindAvailableWordData()
    {
        //Get all WprdData
        foreach (var item in canvasWithWordData)
        {
            if(!item.IsActive)
            {
                return item;
            }
        }

        Debug.Log("there is not enougth canvas available");
        return null;
    }

    public Reveal FindAvailableReveal()
    {
        //Get all Reveal
        foreach (var item in canvasWithQuestionData)
        {
            if (!item.IsActive)
            {
                return item;
            }
        }
        return null;
    }

    public Reveal Entry()
    {
        return entry;
    }

    public void DisableAnswers(FormData type, int id)
    {
        switch (type)
        {
            case FormData.age:
                AnswerManager.Instance.ageIsAnswered = true;
                AnswerManager.Instance.DisableGOIn(AnswerManager.Instance.age);
                break;

            case FormData.adress:
                AnswerManager.Instance.adressIsAnswer = true;
                AnswerManager.Instance.DisableGOIn(AnswerManager.Instance.adress);
                break;

            case FormData.situation:
                for (int i = 0; i < AnswerManager.Instance.situations.Count; i++)
                {
                    if (id == AnswerManager.Instance.situations[i].id)
                    {
                        for (int y = 0; y < AnswerManager.Instance.situations[i].canvas.Count; y++)
                        {
                            AnswerManager.Instance.DisableGOIn(AnswerManager.Instance.situations[i].canvas);
                        }
                    }
                }
                break;
        }

    }
}
