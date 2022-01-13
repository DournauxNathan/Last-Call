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

    [Header("Refs")]
    public Transform checkListTransform = null;
    public Transform descriptionTransform = null;
    [SerializeField] private Transform orderListTransform = null;
    [Space(5)]
    [SerializeField] private Transform pullingStock = null;

    public CanvasGroup leftScreen;
    public CanvasGroup rightScreen;

    [Header("Fade parameters")]
    [SerializeField, Tooltip("Hide UI at this value")] private float beginFadeOutAt;
    [SerializeField, Tooltip("Show UI at this value")] private float beginFadeInAt;

    private bool fadeOut = false;
    private bool fadeIn = false;


    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneLoader.Instance.GetCurrentScene().name == "Office")
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
        }      

        //EventSystem.current.firstSelectedGameObject = checkListTransform.GetChild(0).gameObject;
    }

    public void Update()
    {
        if (unlockImaginaryTransition)
        {
            activateButton.SetActive(true );
            unlockImaginaryTransition = !unlockImaginaryTransition;
        }

        if (MasterManager.Instance.projectionTransition.range <= beginFadeOutAt)
        {
            HideUI();
        }

        if (MasterManager.Instance.projectionTransition.range == beginFadeInAt)
        {
            ShowUI();
        }

        if (fadeIn) //Show UI
        {
            if (UIManager.Instance.leftScreen != null)
            {
                StartFadeIn(UIManager.Instance.leftScreen);
                StartFadeIn(UIManager.Instance.rightScreen);
            }
        }

        if (fadeOut) //Hide UI
        {
            if (UIManager.Instance.leftScreen != null)
            {
                StartFadeOut(UIManager.Instance.leftScreen);
                StartFadeOut(UIManager.Instance.rightScreen);
            }
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


    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    public void StartFadeIn(CanvasGroup uiGroupToFade)
    {
        if (uiGroupToFade.alpha < 1)
        {
            uiGroupToFade.alpha += Time.deltaTime;

            if (uiGroupToFade.alpha >= 1)
            {
                fadeIn = false;
            }
        }
    }

    public void StartFadeOut(CanvasGroup uiGroupToFade)
    {
        if (uiGroupToFade.alpha >= 0)
        {
            uiGroupToFade.alpha -= Time.deltaTime;

            if (uiGroupToFade.alpha == 0)
            {
                fadeOut = false;
            }
        }
    }

}
