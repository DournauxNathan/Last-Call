using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    public List<QuestionFormat> protocoleQuestions;
    public List<QuestionFormat> descriptionQuestion;
    public List<InstantiableButton> buttons;

    [Header("Refs - Questions Section")]
    public Transform checkListTransform = null;
    public Transform descriptionTransform = null;

    [Header("Refs - Order Section")]
    [SerializeField] private Transform orderListTransform = null;
    [Space(5)]
    [SerializeField] private Transform pullingStock = null;

    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;

    public CanvasGroup leftScreen;
    public CanvasGroup rightScreen;

    // Start is called before the first frame update
    void Start()
    {
        ScenarioManager.Instance.LoadScenario();

        activateButton.SetActive(false);

        // A CHANGER QUAND SWITCH ENTRE REA ET IMA
        if (OrderController.Instance.orders.Count == 0)
        {
            for (int i = 0; i < protocoleQuestions.Count; i++)
            {
                var but = FindAvailableButtonForQuestion(protocoleQuestions[i], checkListTransform);
            }

            for (int i = 0; i < descriptionQuestion.Count; i++)
            {
                var but = FindAvailableButtonForQuestion(descriptionQuestion[i], descriptionTransform);
            }
        }
        
        if (OrderController.Instance.isResolve)
        {
            for (int i = 0; i < OrderController.Instance.orders.Count; i++)
            {
                var but = FindAvailableButtonForOrder(OrderController.Instance.orders[i]);
            }
        }

        EventSystem.current.SetSelectedGameObject(checkListTransform.GetChild(0).GetComponentInChildren<Button>().gameObject);
        //EventSystem.current.firstSelectedGameObject = checkListTransform.GetChild(0).gameObject;
    }


    public void Update()
    {
        if (unlockImaginaryTransition)
        {
            activateButton.SetActive(true );
            unlockImaginaryTransition = !unlockImaginaryTransition;
        }
    }

    public InstantiableButton FindAvailableButtonForQuestion(QuestionFormat question, Transform _transform)
    {
        if (question != null)
        {
            foreach (var but in buttons)
            {
                if (!but.isInstiantiated)
                {
                    but.ActivateQuestion(_transform, pullingStock, question);
                    return but;
                }
            }
        }

        Debug.LogError("Not enough buttons");
        return null;
    }

    public InstantiableButton FindAvailableButtonForOrder(OrderFormat order)
    {
        if (order != null)
        {
            foreach (var but in buttons)
            {
                if (!but.isInstiantiated)
                {
                    but.ActivateOrder(orderListTransform, pullingStock, order);
                    return but;
                }
            }
        }

        Debug.LogError("Not enough buttons");
        return null;
    }
}
