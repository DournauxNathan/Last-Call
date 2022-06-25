using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WordData : MonoBehaviour
{
    public TMP_Text text;
    public XRGrabInteractableWithAutoSetup xrGrab;
    public BoxCollider _collider;

    private Transform parentTransform;
    private Transform pullingStock;
    private bool isCorrectAnswer;
    private Answer answer;

    private bool isActive;
    public bool IsActive => isActive;

    private float x, y, z;
    Vector3 pos;
    public bool simulateInput;

    private void Start()
    {
        _collider.enabled = false;
    }

    private void Update()
    {
        if (simulateInput)
        {
            simulateInput = !simulateInput;

            GetComponent<ShakeWord>().isDecaying = true;
        }
    }

    public Answer GetAnswer()
    {
        return this.answer;
    }

    public void Activate(Transform parent, Transform stock, bool isCorrect, string i, Answer _answer)
    {
        this.answer = _answer;

        this.isCorrectAnswer = isCorrect;
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.pullingStock = stock;
        isActive = true;
        
        UpdateText(i);

        xrGrab.enabled = true;
        _collider.enabled = true;
        //GetComponent<ShakeWord>().submitWord.AddListener(UnpauseAudio);

        GetComponent<RectTransform>().localPosition = GetRandomPosition();
    }

    public void UnpauseAudio()
    {
        TimeSettings.Instance.isAudioPause = false;
        TimeSettings.Instance.StartGlobalTimer();

        MasterManager.Instance.references.mainAudioSource.UnPause();
    }

    public void Activate(Transform parent, Transform stock,string i)
    {
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.pullingStock = stock;
        isActive = true;

        UpdateText(i);

        xrGrab.enabled = true;
        _collider.enabled = true;
        GetComponent<RectTransform>().localPosition = GetRandomPosition();
    }

    public Vector3 GetRandomPosition()
    {
        x = Random.Range(-.1f, .1f);
        y = Random.Range(-0.24f, .24f);
        z = Random.Range(-0.15f, .15f);
        
        return pos = new Vector3(x, y, z);
    }

    private void UpdateText(string i)
    {
        if (isActive)
        {
            text.text = i;
        }
    }

    public void Deactivate()
    {
        text.text = string.Empty;
        
        isActive = false;
        transform.SetParent(pullingStock);

        GetComponent<RectTransform>().localPosition = new Vector3(0, -170f, 0);

        xrGrab.enabled = false;
        _collider.enabled = false;

        GetComponent<CanvasGroup>().alpha = 1;
    }

    public void SubmitAnswer()
    {
        if (isCorrectAnswer)
        {
            UIManager.Instance.UpdateForm(answer.type, text.text);
            UnpauseAudio();
            WordManager.Instance.DisableAnswers(answer.type, answer.id);
        }
        else
        {
            UIManager.Instance.UpdateForm(answer.type, text.text);
            UnpauseAudio();
            WordManager.Instance.DisableAnswers(answer.type, answer.id);

            Debug.Log("Give penalty");
        }
    }

    public void SimulateInput(bool value)
    {
        simulateInput = true;
    }

}
