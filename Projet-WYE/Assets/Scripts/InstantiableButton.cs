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
    private Order order;
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

    public void ActivateOrder(Transform parent, Transform stock, Order order)
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
            UnitDispatcher.Instance.AddToUnlock(question.units[currentClick]);

            for (int i = 0; i < question.listIdObject.Length; i++)
            {
                if (currentClick == question.listIdObject[i].y && question.listIdObject[i].x != 0)
                {
                    UIManager.Instance.Ask();
                    swapImaginaire.indexesList.Add(Mathf.FloorToInt(question.listIdObject[i].x));
                }
            }
            SendAnswer(currentClick); //envoi le string
            SubTitle.Instance.DisplaySub(question.listQuestion[currentClick], question.voiceLineQuestion.Length, question.listAnswers[currentClick], question.voiceLineAnswer.Length); //A TEST
            currentClick++;
        }
        else if (currentClick >= question.listQuestion.Length - 1)
        {
            //Active unitée, boucle infinit quand click
            UnitDispatcher.Instance.AddToUnlock(question.units[currentClick]);

            for (int i = 0; i < question.listIdObject.Length; i++)
            {
                if (currentClick == question.listIdObject[i].y && !swapImaginaire.indexesList.Contains(question.listIdObject[i].x) && question.listIdObject[i].x != 0)
                {
                    UIManager.Instance.Ask();
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
            SendAnswer(currentClick); //envoi le string
            SubTitle.Instance.DisplaySub(question.listQuestion[currentClick], question.voiceLineQuestion.Length, question.listAnswers[currentClick], question.voiceLineAnswer.Length);
            Desactivate();
        }

        UpdateQuestion();
    }

    public void SendOrder()
    {
        ScenarioManager.Instance.UpdateEndingsValue(order.influence);
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
            text.text = order.order;
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
    }

    IEnumerator PlayQuestionAudio(float time, int current)
    {                   
        UIManager.Instance.ToggleButton();

        audioSource.clip = question.voiceLineQuestion[current];
        audioSource.Play();
        
        yield return new WaitForSeconds(time );
        
        StartCoroutine(PlayAnswerAudio(question.voiceLineAnswer.Length, current));

    }

    IEnumerator PlayAnswerAudio(float time, int current)
    {
        yield return new WaitForSeconds(time);
        
        audioSource.clip = question.voiceLineAnswer[current];
        audioSource.Play();
        
        UIManager.Instance.ToggleButton();
    }

    private void SendAnswer(int i)
    {
        SaveQuestion.Instance.AddQuestion(question.listQuestion[i]);
    }

    public void IsAnswered()
    {
        if (question != null)
        {
            for (int i = 0; i < question.listQuestion.Length; i++)
            {
                if (SaveQuestion.Instance.AlreadyAnswerd(question.listQuestion[i]))
                {
                    Debug.Log("Desactivate: " + question.listQuestion[i]);
                    Desactivate();
                }
            }
        }
        
    }

}
