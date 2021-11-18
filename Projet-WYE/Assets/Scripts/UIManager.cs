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

    public GameObject listeAEnvoyer;
    private ObjetcActivatorImaginaire swapImaginaire;

    // Start is called before the first frame update
    void Start()
    {
        swapImaginaire = GameObject.Find("ObjetAactiver").GetComponent<ObjetcActivatorImaginaire>();

        for (int i = 0; i < buttons.Length; i++)
        {
            questionData[i].currentClick = 0;
            buttons[i].GetComponentInChildren<Text>().text = questionData[i].listQuestion[questionData[i].currentClick];
            Dico.Add(buttons[i], questionData[i]);
            //Debug.Log(buttons[i].gameObject.name + " " + questionData[i].listeDeQuestion[questionData[i].currentClick]);
        }
    }

    public void IncreasedClick()
    {
        foreach (var button in Dico)
        {
            if (EventSystem.current.currentSelectedGameObject == button.Key.gameObject)
            {
                if (button.Value.currentClick != button.Value.listQuestion.Length - 1)
                {
                    button.Value.currentClick++;
                    for (int i = 0; i < button.Value.listIdObject.Length; i++)
                    {
                        if (button.Value.currentClick == button.Value.listIdObject[i].y)
                        {
                            swapImaginaire.listeIndex.Add(Mathf.FloorToInt(button.Value.listIdObject[i].x));
                        }
                    }
                }
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = questionData[i].listQuestion[questionData[i].currentClick];

        }
    }
}
