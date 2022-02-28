using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubTitle : Singleton<SubTitle>
{

    public TMP_Text main_Text;
    private bool isQuestion = false;

    // Start is called before the first frame update
    void Start()
    {
        ClearText(); // Clean
        //DisplaySub("text1", 3f, "text2", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearText()
    {
        main_Text.text = "";
    }

    IEnumerator DisplaySubTitle(float time)
    {
        yield return new WaitForSeconds(time);
        ClearText();
    }
    IEnumerator DisplaySubTitle(float time,float time2,string text)
    {
        yield return new WaitForSeconds(time);
        ClearText();
        main_Text.text = text;
        if (isQuestion)
        {
            StartCoroutine(DisplaySubTitle(time2));
            isQuestion = false;
        }
    }





    public void DisplaySub(string str,float clipLength)
    {
        ClearText();
        main_Text.text = str;
        StartCoroutine(DisplaySubTitle(clipLength));
    }

    public void DisplaySub(string question, float questionLength, string answer,float answerLength) 
    {
        isQuestion = true;
        ClearText(); //Debug.Log("Clear");
        main_Text.text = question; //Debug.Log("question");
        StartCoroutine(DisplaySubTitle(questionLength,answerLength,answer)); 
    }




}
