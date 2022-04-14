using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
public class UIManager : Singleton<UIManager>
{
    [Header("Screens Canvas")]
    public CanvasGroup[] _canvasGroup;

    [Header("Emergency Reports Form")]
    public Form currentForm;
    private FormData _formData;

    [Header("Unit Manager Feedbacks")]
    public List<GameObject> unitDispatcherFeedbacks;

    public ParticleSystem smoke;

    public void UpdateForm(FormData _answerType, string data)
    {
        switch (_answerType)
        {
            case FormData.name:
                currentForm.nameField.text = data.ToString();
                break;

            case FormData.age:
                currentForm.ageField.text = data.ToString();
                break;

            case FormData.adress:
                currentForm.adressField.text = data.ToString();
                break;

            case FormData.situation:
                currentForm.situationField.text += data.ToString();
                break;

            case FormData.unit:
                currentForm.unitField.text += data.ToString();
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

    public void UpdateUnitManager(int i)
    {
        switch (i)
        {
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
}

public enum Fadetype
{
    In,
    Out
}