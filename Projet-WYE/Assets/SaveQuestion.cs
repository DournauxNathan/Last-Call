using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestion : Singleton<SaveQuestion>
{

    public List<string> AnsweredQuestions;


    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp()
    {
        AnsweredQuestions.Clear();
    }

    public void AddQuestion(string _question)
    {
        if (!AnsweredQuestions.Contains(_question))
        {
            AnsweredQuestions.Add(_question);
        }
    }

    public void AddQuestion(List<string> _questions)
    {
        foreach (var q in _questions)
        {
            if (!AnsweredQuestions.Contains(q))
            {
                AnsweredQuestions.Add(q);
            }
        }
    }

    public bool AlreadyAnswerd(string _question)
    {
        if (AnsweredQuestions.Contains(_question))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
