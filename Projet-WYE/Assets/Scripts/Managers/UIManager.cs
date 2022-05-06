using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UIManager : Singleton<UIManager>
{
    [Header("Screens Canvas")]
    public CanvasGroup[] _canvasGroup;

    [Header("Incoming & Outcoming call")]
    public GameObject incomingAsset;
    public GameObject outcomingAsset;

    [Header("Emergency Reports Form")]
    public Form currentForm;
    private FormData _formData;

    [Header("Unit Manager Feedbacks")]
    public List<GameObject> unitDispatcherFeedbacks;

    [Header("Text Writing Effect")]
    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.1f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    public ParticleSystem smoke;

    public void UpdateForm(FormData _answerType, string data)
    {
        switch (_answerType)
        {
            case FormData.name:
                StartCoroutine(TypeWriterTMP(currentForm.nameField, data.ToString()));
                break;

            case FormData.age:
                StartCoroutine(TypeWriterTMP(currentForm.ageField, data.ToString()));
                break;

            case FormData.adress:
                StartCoroutine(TypeWriterTMP(currentForm.adressField, data.ToString()));
                break;

            case FormData.situation:
                StartCoroutine(TypeWriterTMP(currentForm.situationField, data.ToString()));
                break;

            case FormData.unit:
                if (!currentForm.unitField.text.Contains(data.ToString()))
                {
                    StartCoroutine(TypeWriterTMP(currentForm.unitField, data.ToString()));
                }
                break;
        }

        if (currentForm.nameField.text != string.Empty && currentForm.ageField.text != string.Empty
            && currentForm.adressField.text != string.Empty && currentForm.situationField.text != string.Empty
            && currentForm.unitField.text != string.Empty)
        {
            SetFormToComplete();
        }
    }
    public void SetFormToComplete()
    {
        currentForm.isComplete = true;
        currentForm.stamp.enabled = true;
    }
    public void Fade(Fadetype type)
    {
        switch (type)
        {
            case Fadetype.In:
                foreach (CanvasGroup canvas in _canvasGroup)
                {
                    if (canvas.alpha < 1)
                    {
                        canvas.alpha += Time.deltaTime;

                        if (canvas.alpha >= 1)
                        {
                            smoke.Play();
                        }
                    }
                }
                break;

            case Fadetype.Out:
                foreach (CanvasGroup canvas in _canvasGroup)
                {
                    if (canvas.alpha > 0)
                    {
                        canvas.alpha -= Time.deltaTime;

                        if (canvas.alpha <= 0)
                        {
                            smoke.Play();
                        }
                    }
                }
                break;
        }
    }
    public void Fade(Fadetype type, CanvasGroup _canvas)
    {
        switch (type)
        {
            case Fadetype.In:
                if (_canvas.alpha < 1)
                {
                    _canvas.alpha += Time.deltaTime;
                }
                break;

            case Fadetype.Out:
                if (_canvas.alpha > 0)
                {
                    _canvas.alpha -= Time.deltaTime;
                }
                break;
        }
    }
    public void UpdateUnitManager(int i)
    {
        switch (i)
        {
            case 0:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 1:
                unitDispatcherFeedbacks[0].SetActive(true);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 2:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(true);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 3:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(true);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 4:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(true);
                break;
        }
    }
    public void InComingCall(bool isActive)
    {
        incomingAsset.SetActive(isActive);
    }
    public void OutComingCall(bool isActive)
    {
        outcomingAsset.SetActive(isActive);
    }
    IEnumerator TypeWriterTMP(TMP_Text _tmpProText, string _writer)
    {
        _tmpProText.text += leadingCharBeforeDelay ? leadingChar : " ";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in _writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }
    }
}

public enum Fadetype
{
    In,
    Out
}