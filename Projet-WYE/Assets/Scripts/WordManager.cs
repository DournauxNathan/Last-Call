using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : Singleton<WordManager>
{
    public SphereCollider spawner;
    public Transform getTransfrom;
    public Transform stockA, stockB;

    public List<Answer> answers;
    public List<Question> questions;

    public List<WordData> canvasWithWordData;
    public List<Reveal> canvasWithQuestionData;

    private void Start()
    {
        MasterManager.Instance.currentPhase = Phases.Phase_2;
        MasterManager.Instance.isInImaginary = true;
        
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
                    item.Activate(transform, stockA, answer.keywords[i].isCorrectAnswer, answer.keywords[i].proposition);
                }
            }
        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_2 && MasterManager.Instance.isInImaginary)
        {
            foreach (Question question in questions)
            {
                //Get the keywords in the answer
                for (int i = 0; i < question.question.Count; i++)
                {
                    //Find any available Canvas Word 
                    var item = FindAvailableReveal();
                    //if true, Activate Canvas Word and Set his text with the current propo
                    item.Activate(transform, stockA, question, question.question[i].text, i);
                }
            }
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
