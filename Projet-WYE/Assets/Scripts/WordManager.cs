using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : Singleton<WordManager>
{
    public SphereCollider spawner;
    public Transform getTransfrom;
    public Transform stockA, stockB;

    [HideInInspector] public List<Answer> answers;
    [HideInInspector] public List<Question> questions;

    public List<WordData> canvasWithWordData;
    public List<Reveal> canvasWithQuestionData;

    public bool isProtocolComplete;

    private void Update()
    {
        if (isProtocolComplete && MasterManager.Instance.currentPhase == Phases.Phase_1)
        {
            ProtocolComplete();
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
                }
            }

        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_3 && !MasterManager.Instance.isInImaginary)
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

    public void ProtocolComplete()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<WordData>().Deactivate();
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
}
