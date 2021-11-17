using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public QuestionFormat[] questionData;
    public Button[] buttons;
    Dictionary<Button, QuestionFormat> Dico = new Dictionary<Button, QuestionFormat>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = questionData[i].listeDeQuestion[questionData[i].currentClick];
            Dico.Add(buttons[i], questionData[i]);
            Debug.Log(buttons[i].gameObject.name + " " + questionData[i].listeDeQuestion[questionData[i].currentClick]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void IncreasedClick()
    {
        /*foreach (var button in buttons)
        {
            if (EventSystem.current.currentSelectedGameObject == button.gameObject)
            {


                // button.GetComponentInChildren<Text>().text = questionData[]
            }
        }*/

        foreach (var button in Dico)
        {
            if (EventSystem.current.currentSelectedGameObject == button.Key.gameObject)
            {
                button.Value.currentClick++;
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = questionData[i].listeDeQuestion[questionData[i].currentClick];

        }
    }




}
