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

    [Header("Refs - Phase 1")]
    public List<InstantiableButton> buttons;
    public Transform checkListTransform = null;
    public Transform descriptionTransform = null;
    [Space(5)]
    [SerializeField] private Transform pullingStock = null;
    public CanvasGroup leftScreen;

    [Header("Refs - Phase 2")]
    public List<InstantiableButton> _buttons;
    public Transform orderListTransform = null;
    [Space(5)]
    [SerializeField] private Transform pullingStockB = null;
    public CanvasGroup rightScreen;

    [Header("Fade parameters")]
    [SerializeField, Tooltip("Hide UI at this value")] private float beginFadeOutAt;
    [SerializeField, Tooltip("Show UI at this value")] private float beginFadeInAt;

    private bool fadeOut = false;
    private bool fadeIn = false;

    [Header("Debug, Transition to Imaginaire")]
    [SerializeField] private int nQuestionAnswer;
    [SerializeField] private GameObject activateButton;
    [SerializeField] private bool unlockImaginaryTransition = false;
    public ParticleSystem smoke;

    public GameObject startSelectbutton;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startSelectbutton.GetComponentInChildren<Button>().gameObject);

        if (MasterManager.Instance.isTutoEnded || MasterManager.Instance.skipTuto)
        {            
            LoadQuestions();
            PullQuestion();
        }

        CheckButtons();
    }

    public void PullQuestion()
    {
        if (ScenarioManager.Instance.isScenarioLoaded)
        {            
            ScenarioManager.Instance.isScenarioLoaded = false;

            activateButton.SetActive(false);

            for (int i = 0; i < protocoleQuestions.Count; i++)
            {
                var but = FindAvailableButtonForQuestion(protocoleQuestions[i], checkListTransform);
            }

            for (int i = 0; i < descriptionQuestion.Count; i++)
            {
                var but = FindAvailableButtonForQuestion(descriptionQuestion[i], descriptionTransform);
            }

            StartCoroutine(ExecuteAfterTime(.5f));
        }

        if (MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            /*if (OrderController.Instance.GetResolve())
            {*/
                for (int i = 0; i < OrderController.Instance.ordersStrings.Count; i++)
                {
                    var but = FindAvailableButtonForOrder(OrderController.Instance.ordersStrings[i]);
                }
            //}

            UpdateEventSystem(orderListTransform);
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        EventSystem.current.SetSelectedGameObject(checkListTransform.GetChild(0).GetComponentInChildren<Button>().gameObject);
        //UpdateEventSystem(checkListTransform);
    }

    public void UpdateEventSystem(Transform transform)
    {
        if (transform.name == checkListTransform.name)
        {
            for (int i = checkListTransform.childCount - 1; i >= 0; i--)
            {
                if (checkListTransform.GetChild(i).GetComponentInChildren<Button>().enabled)
                {
                    EventSystem.current.SetSelectedGameObject(checkListTransform.GetChild(i).GetComponentInChildren<Button>().gameObject);
                }
            }
        }

        if (transform.name == descriptionTransform.name)
        {
            for (int i = descriptionTransform.childCount - 1; i >= 0; i--)
            {
                if (descriptionTransform.GetChild(i).GetComponentInChildren<Button>().enabled)
                {
                    EventSystem.current.SetSelectedGameObject(descriptionTransform.GetChild(i).GetComponentInChildren<Button>().gameObject);
                }
            }
        }

        if (transform.name == orderListTransform.name)
        {
            for (int i = orderListTransform.childCount - 1; i >= 0; i--)
            {
                if (orderListTransform.GetChild(i).GetComponentInChildren<Button>().enabled)
                {
                    EventSystem.current.SetSelectedGameObject(orderListTransform.GetChild(i).GetComponentInChildren<Button>().gameObject);
                }
            }
        }
    }

    public void Update()
    {
        if (unlockImaginaryTransition)
        {
            activateButton.SetActive(true);
            unlockImaginaryTransition = !unlockImaginaryTransition;
        }

        if (Projection.Instance.transitionValue <= beginFadeOutAt)
        {
            HideUI();
            smoke.Stop();
        }

        if (Projection.Instance.transitionValue >= beginFadeInAt)
        {
            ShowUI();

            smoke.Play();
        }

        if (fadeIn) //Show UI
        {
            if (leftScreen != null)
            {
                StartFadeIn(leftScreen);

                if (MasterManager.Instance.currentPhase == Phases.Phase_3)
                {
                    StartFadeIn(rightScreen);
                }
            }
        }

        if (fadeOut) //Hide UI
        {
            StartFadeOut(leftScreen);
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
    public InstantiableButton FindAvailableButtonForOrder(Order order)
    {
        if (order != null)
        {
            foreach (var but in _buttons)
            {
                if (!but.isInstiantiated)
                {
                    but.ActivateOrder(orderListTransform, pullingStockB, order);
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

    public void Ask()
    {
        nQuestionAnswer++;
    } 

    public bool CheckAnswer()
    {        
        if (nQuestionAnswer > descriptionQuestion.Count)
        {
            return MasterManager.Instance.canImagine = true;
        }
        else
        {
            return MasterManager.Instance.canImagine = false;
        }
    }

    public void ToggleButton()
    {
        /*for (int i = 0; i < checkListTransform.childCount; i++)
        {
            checkListTransform.GetChild(i).GetComponent<Button>().interactable = !checkListTransform.GetChild(i).GetComponent<Button>().interactable;
        }

        for (int i = 0; i < descriptionTransform.childCount; i++)
        {
            descriptionTransform.GetChild(i).GetComponent<Button>().interactable = !descriptionTransform.GetChild(i).GetComponent<Button>().interactable;
        }*/

        foreach (var button in buttons)
        {
            button.gameObject.GetComponentInChildren<Button>().enabled = !button.gameObject.GetComponentInChildren<Button>().enabled;

            /*if (button.gameObject.GetComponentInChildren<Button>().enabled)
            {
                button.gameObject.GetComponentInChildren<Button>().colors = 
            }
            else
            {
                button.gameObject.GetComponentInChildren<Button>().colors = ColorBlock.defaultColorBlock;
            }*/
        }
    }

    public void LoadQuestions()
    {
        switch (ScenarioManager.Instance.currentScenario)
        {
            case Scenario.TrappedMan:
                descriptionQuestion.AddRange(ScenarioManager.Instance.trappedMan);
            break;

            case Scenario.HomeInvasion:
                descriptionQuestion.AddRange(ScenarioManager.Instance.homeInvasion);
            break;

            case Scenario.DomesticAbuse:
                descriptionQuestion.AddRange(ScenarioManager.Instance.domesticAbuse);
            break;

            case Scenario.RisingWater:
                    descriptionQuestion.AddRange(ScenarioManager.Instance.risingWater);
            break;
        }        

        ScenarioManager.Instance.isScenarioLoaded = true;
    }

    //Check si les questions ont déjà été répondu entre les transition
    public void CheckButtons()
    {
        foreach (var button in buttons)
        {
            button.IsAnswered();
        }
    }
}
