using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public List<QuestionFormat> questionData;
    public List<InstantiableButton> buttons;

    [Header("References")]
  
    [SerializeField] private Transform checkListTransform = null;
    [SerializeField] private Transform pullingStock = null;


    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        activateButton.SetActive(false);


        for (int i = 0; i < questionData.Count; i++)
        {
            var but = FindAvailableButton(questionData[i]);
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
    public InstantiableButton FindAvailableButton(QuestionFormat question)
    {
        foreach (var but in buttons)
        {
            if(!but.isInstiantiated)
            {
                but.Activate(checkListTransform, pullingStock, question);
                return but;
            }
        }
        Debug.LogError("Not enough buttons");
        return null;
    }
}
