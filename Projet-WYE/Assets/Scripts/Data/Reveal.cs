using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reveal : MonoBehaviour
{
    public TMP_Text text;

    public float amount = 0;

    private Transform parentTransform;
    private Transform pullingStock;
    private bool isCorrectAnswer;

    private Question question;
    public Question _question;
    public int atIndex;

    private bool isActive;
    public bool IsActive => isActive;

    private float x, y, z;
    Vector3 pos;

    public bool isEntryQMPLoaded;

    private QuestionData currentQuestion => question.questions[atIndex];

    public bool simulateInput;
    private bool isReveal;

    public void InitEntry()
    {
        if (!MasterManager.Instance.envIsReveal && isEntryQMPLoaded)
        {
            _question = ScenarioManager.Instance.currentScenarioData.callerInformations.adress;
            UpdateText(_question.questions[0].question);

            Debug.Log(_question);
        }
    }
    
    private void Update()
    {
        if (ScenarioManager.Instance.currentScenario != Scenario.None)
        {
            InitEntry();
        }


        if (simulateInput)
        {
            simulateInput = !simulateInput;

            GetComponent<ShakeWord>().isDecaying = true;
        }
    }

    public void Activate(Transform parent, Transform stock, Question _question, string i, int _index)
    {
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.question = _question;
        this.atIndex = _index;

        this.pullingStock = stock;
        isActive = true;

        UpdateText(i);


        GetComponent<XRGrabInteractableWithAutoSetup>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;

        GetComponent<RectTransform>().localPosition = GetRandomPosition();
    }

    public void Activate(Transform parent, Transform stock, Question _question, string i)
    {
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.question = _question;

        this.pullingStock = stock;
        isActive = true;

        UpdateText(i);

        GetComponent<RectTransform>().localPosition = GetRandomPosition();
    }

    public Vector3 GetRandomPosition()
    {
        x = Random.Range(-.1f, .1f);
        y = Random.Range(-0.02f, -0.15f);
        z = Random.Range(-0.06f,- 0.23f);

        return pos = new Vector3(x, y, z);
    }

    private void UpdateText(string i)
    {
        if (isActive)
        {
            text.text = i;
        }
    }

    public void SubmitAnswer()
    {
        if (_question == null)
        {
            MasterManager.Instance.references.mainAudioSource.PlayOneShot(question.questions[atIndex].voices);
        }
        StartCoroutine(Show());
    }

    public void Deactivate()
    {
        text.text = string.Empty;
        isActive = false;
        transform.SetParent(pullingStock);


        GetComponent<XRGrabInteractableWithAutoSetup>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        GetComponent<CanvasGroup>().alpha = 1;

        //GetComponent<RectTransform>().localPosition = Vector3.zero; 
    }


    public void DisplayQuestions()
    {
        foreach (var item in WordManager.Instance.questionsGo)
        {
            item.SetActive(true);
        }
    }


    public IEnumerator Show()
    {
        while (true)
        {
            amount += Time.deltaTime * Projection.Instance.time;

            if (_question != null)
            {
                foreach (var item in _question.questions[atIndex].linkObjects)
                {
                    item.SetFloat("_Dissolve", amount);
                }
            }
            else
            {
                foreach (var item in question.questions[atIndex].linkObjects)
                {
                    item.SetFloat("_Dissolve", amount);
                }
            }



            if (amount > 50f)
            {
                amount = 50f;

                if (MasterManager.Instance.currentPhase == Phases.Phase_0)
                {
                    isReveal = true;
                }

                StopAllCoroutines();

                yield return null;
            }

            yield return null;
        }
    }

    public void SimulateInput(bool value)
    {
        simulateInput = true;
    }

}