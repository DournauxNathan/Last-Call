using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class InstantiableButton : MonoBehaviour
{
    public bool simulateInput = false;

    private AudioSource audioSource;

    [Header("Refs")]
    public Button button;
    public Image img;
    public TMP_Text text;
    public Toggle toggle;
    public Image toggleImg;

    [HideInInspector] public bool isInstiantiated;

    public int currentClick;
    private QuestionFormat question;
    private OrderFormat order;
    private ObjectActivator swapImaginaire;
    private Transform stock;
    private bool isActive;

    private void Start()
    {
        swapImaginaire = MasterManager.Instance.objectActivator;
        audioSource = MasterManager.Instance.mainAudioSource;
    }

    private void Update()
    {
        if (simulateInput)
        {
            SetSimulateInput(false);
            IncreaseClick();
            PlayQuestionAnswer();
        }
    }

    public void SetSimulateInput(bool b)
    {
        simulateInput = b;
    }

    public void ActivateQuestion(Transform parent, Transform stock, QuestionFormat question)
    {
        transform.SetParent(parent);

        this.stock = stock;
        this.question = question;

        currentClick = 0;
        button.enabled = true;
        button.interactable = true;
        img.enabled = true;
        toggle.isOn = false;
        toggle.enabled = true;
        toggleImg.enabled = true;
        isActive = true;

        isInstiantiated = true;

        UpdateQuestion();
    }

    public void ActivateOrder(Transform parent, Transform stock, OrderFormat order)
    {
        transform.SetParent(parent);

        this.stock = stock;
        this.order = order; 

        currentClick = 0;
        button.enabled = true;
        button.interactable = true;
        img.enabled = true;
        toggle.enabled = true;
        toggleImg.enabled = true;
        isActive = true;

        isInstiantiated = true;

        UpdateOrder();
    }

    public void IncreaseClick()
    {
        int currentBtn = 0;

        if (currentClick < question.listQuestion.Length - 1)
        {
            //Active unitée
            //Debug.Log(button.Value.units[button.Value.currentClick]);
            UnitDispatcher.Instance.AddToUnlock(question.units[currentClick]);

            for (int i = 0; i < question.listIdObject.Length; i++)
            {
                if (currentClick == question.listIdObject[i].y && question.listIdObject[i].x != 0)
                {
                    swapImaginaire.indexesList.Add(Mathf.FloorToInt(question.listIdObject[i].x));
                }
            }
            currentClick++;
        }
        else if (currentClick >= question.listQuestion.Length - 1)
        {
            //Active unitée, boucle infinit quand click
            //Debug.Log(button.Value.units[button.Value.currentClick]);
            UnitDispatcher.Instance.AddToUnlock(question.units[currentClick]);

            for (int i = 0; i < question.listIdObject.Length; i++)
            {
                if (currentClick == question.listIdObject[i].y && !swapImaginaire.indexesList.Contains(question.listIdObject[i].x) && question.listIdObject[i].x != 0)
                {
                    //Debug.Log(button.Value.listIdObject[i].x);
                    swapImaginaire.indexesList.Add(Mathf.FloorToInt(question.listIdObject[i].x));
                }
                currentBtn = i;
            }

            if (EventSystem.current.gameObject != UIManager.Instance.checkListTransform.GetChild(currentBtn).GetComponentInChildren<Button>().gameObject)
            {
                EventSystem.current.SetSelectedGameObject(UIManager.Instance.checkListTransform.GetChild(currentBtn).GetComponentInChildren<Button>().gameObject);
            }
            else if (EventSystem.current.gameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(UIManager.Instance.checkListTransform.GetChild(0).GetComponentInChildren<Button>().gameObject);
            }

            Desactivate();
        }

        UpdateQuestion();
    }

    public void SendOrder()
    {
        ScenarioManager.Instance.UpdateEndingsValue(order.endingModifier);
        //Play audio in the order format
        Desactivate();
    }

    public void Desactivate()
    {
        //isActive = false;
        //this.gameObject.SetActive(false);
        ReputOnStock();
    }
    public virtual void ReputOnStock()
    {
        button.enabled = false;
        toggle.enabled = false;
        toggle.isOn = true;


        //transform.SetParent(stock);
        //isInstiantiated = false;
    }
    private void UpdateQuestion()
    {
        if(isActive)
        {
            text.text = question.listQuestion[currentClick];
        }
        else
        {
            text.text = string.Empty;
            button.interactable = false;
        }
    }
    private void UpdateOrder()
    {
        if (isActive)
        {
            text.text = order.orderText;
        }
        else
        {
            text.text = string.Empty;
            button.interactable = false;
        }
    }

    public void PlayQuestionAnswer()
    {
        StartCoroutine(PlayQuestionAudio(question.voiceLineQuestion.Length, currentClick));
       
        /* 
         * float _temp =0f
         * float hardTimer = 0.5f
         * int current
         * _temp = question.voiceLineQuestion.Length + question.voiceLineAnswer.Length + hardTimer
         * Debug.Log(question.voiceLineQuestion.Length + question.voiceLineAnswer.Length + hardTimer
         * StartCoroutine(LockOtherButton(_temp
         * current = currentClick;
         */
    }

    IEnumerator PlayQuestionAudio(float time, int current)
    {   
        //Debug.Log("playing Question for :"+time+"s");
                
        UIManager.Instance.ToggleButton();

        audioSource.clip = question.voiceLineQuestion[current];
        audioSource.Play();
        
        yield return new WaitForSeconds(time );
        
        StartCoroutine(PlayAnswerAudio(question.voiceLineAnswer.Length, current));

    }

    IEnumerator PlayAnswerAudio(float time, int current)
    {
        yield return new WaitForSeconds(time);
        
        //Debug.Log("playing Answer for :" + time + "s");
        
        audioSource.clip = question.voiceLineAnswer[current];
        audioSource.Play();
        
        UIManager.Instance.ToggleButton();
    }

    

}
