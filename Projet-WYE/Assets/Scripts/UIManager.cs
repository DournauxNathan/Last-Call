using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public List<QuestionFormat> questionData;

    [HideInInspector] public List<Button> buttons;
    Dictionary<Button, QuestionFormat> Dico = new Dictionary<Button, QuestionFormat>();

    [Header("References")]
    private int buttonsCount;
    private ObjectActivator swapImaginaire;
    [SerializeField] private Transform checkListTransform = null;
    [SerializeField] private GameObject buttonPrefab;

    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        activateButton.SetActive(false);

        swapImaginaire = GameObject.Find("ObjectActivator").GetComponent<ObjectActivator>();

        buttonsCount = questionData.Count;

        for (int i = 0; i < buttonsCount; i++)
        {
            var _button = Instantiate(buttonPrefab, checkListTransform.transform);
            _button.name = "Button"+i;

            Button b = _button.GetComponent<Button>();

            b.onClick.AddListener(() => IncreasedClick()); //invisible in editor
            buttons.Add(b);

            questionData[i].currentClick = 0;
            buttons[i].GetComponentInChildren<TMP_Text>().text = questionData[i].listQuestion[questionData[i].currentClick];
            Dico.Add(buttons[i], questionData[i]);
            
            //Debug
            //Debug.Log(buttons[i].gameObject.name + " " + questionData[i].listQuestion[questionData[i].currentClick]);
            //Debug.Log(Dico.ContainsKey(buttons[i]));
        }
    }

    public void Update()
    {
        if (unlockImaginaryTransition)
        {
            activateButton.SetActive(true );
            unlockImaginaryTransition = !unlockImaginaryTransition;
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
                    //Active unit�e
                    //Debug.Log(button.Value.units[button.Value.currentClick]);
                    UnitManager.Instance.AddToUnlock(button.Value.units[button.Value.currentClick]);
                    
                    
                    for (int i = 0; i < button.Value.listIdObject.Length; i++)
                    {
                        if (button.Value.currentClick == button.Value.listIdObject[i].y && button.Value.listIdObject[i].x != 0)
                        {
                            swapImaginaire.indexesList.Add(Mathf.FloorToInt(button.Value.listIdObject[i].x));
                        }
                    }
                    button.Value.currentClick++;
                }
                else if(button.Value.currentClick == button.Value.listQuestion.Length-1) 
                {
                    //Active unit�e, boucle infinit quand click
                    //Debug.Log(button.Value.units[button.Value.currentClick]);
                    UnitManager.Instance.AddToUnlock(button.Value.units[button.Value.currentClick]);

                    for (int i = 0; i < button.Value.listIdObject.Length; i++)
                    {
                        if (button.Value.currentClick == button.Value.listIdObject[i].y && !swapImaginaire.indexesList.Contains(button.Value.listIdObject[i].x) && button.Value.listIdObject[i].x != 0)
                        {
                            //Debug.Log(button.Value.listIdObject[i].x);
                            swapImaginaire.indexesList.Add(Mathf.FloorToInt(button.Value.listIdObject[i].x));
                        }
                    }

                    button.Key.gameObject.SetActive(false);
                    
                }
            }
        }

        //Change le texte de tous les boutons
        for (int i = 0; i < buttonsCount; i++)
        {
            buttons[i].GetComponentInChildren<TMP_Text>().text = questionData[i].listQuestion[questionData[i].currentClick];
        }
    }
}
