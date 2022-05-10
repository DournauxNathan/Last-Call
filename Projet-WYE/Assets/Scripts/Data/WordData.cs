using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordData : MonoBehaviour
{
    public TMP_Text text;

    private Transform parentTransform;
    private Transform pullingStock;
    private bool isCorrectAnswer;
    private Answer answer;

    private bool isActive;
    public bool IsActive => isActive;

    private float x, y, z;
    Vector3 pos;
    public bool simulateInput;

    private void Update()
    {
        if (simulateInput)
        {
            simulateInput = !simulateInput;

            GetComponent<ShakeWord>().isDecaying = true;
        }
        if (SherlockEffect.Instance.offsets.Count > 0) // Check if the list is empty
        {
            SherlockEffect.Instance.CheckOffset(transform);
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

        GetComponent<RectTransform>().localPosition = GetRandomPosition();
        GetComponent<RectTransform>().localEulerAngles = Vector3.zero;

        //SherlockEffect.Instance.AddOffset(transform);
    }

    public void Activate(Transform parent, Transform stock,string i)
    {
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.pullingStock = stock;
        isActive = true;

        UpdateText(i);

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
    }

    public void SubmitAnswer()
    {

        WordManager.Instance.DisableAnswers(answer.type, answer.id);

        if (isCorrectAnswer)
        {
            UIManager.Instance.UpdateForm(answer.type, text.text);
        }
        else
        {
            UIManager.Instance.UpdateForm(answer.type, text.text);

            Debug.Log("Give penalty");
        }
    }

    public void SimulateInput(bool value)
    {
        simulateInput = true;
    }

}
