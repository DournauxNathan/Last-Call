using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    public List<QuestionFormat> questionData;
    public List<InstantiableButton> buttons;
    public List<InstantiableButton> buttonsOrder;

    [Header("Refs - Question Section")]
    public Transform checkListTransform = null;
    [SerializeField] private Transform questionPullingStock = null;

    [Header("Refs - Order Section")]
    [SerializeField] private Transform orderListTransform = null;
    [SerializeField] private Transform orderPullingStock = null;

    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;


    // Start is called before the first frame update
    void Start()
    {
        activateButton.SetActive(false);

        ScenarioManager.Instance.LoadScenario();

        // A CHANGER QUAND SWITCH ENTRE REA ET IMA
        if (OrderController.Instance.orders.Count == 0)
        {
            for (int i = 0; i < questionData.Count; i++)
            {
                var but = FindAvailableButtonForQuestion(questionData[i]);
            }
        }
        
        if (OrderController.Instance.isResolve)
        {
            for (int i = 0; i < OrderController.Instance.orders.Count; i++)
            {
                var but = FindAvailableButtonForOrder(OrderController.Instance.orders[i]);
            }
        }

        EventSystem.current.SetSelectedGameObject(checkListTransform.GetChild(0).gameObject);
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

    public InstantiableButton FindAvailableButtonForQuestion(QuestionFormat question)
    {
        if (question != null)
        {
            foreach (var but in buttons)
            {
                if (!but.isInstiantiated)
                {
                    but.ActivateQuestion(checkListTransform, questionPullingStock, question);
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
            foreach (var but in buttonsOrder)
            {
                if (!but.isInstiantiated)
                {
                    but.ActivateOrder(orderListTransform, orderPullingStock, order);
                    return but;
                }
            }
        }

        Debug.LogError("Not enough buttons");
        return null;
    }
}
