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

    private bool isActive;
    public bool IsActive => isActive;

    public void Activate(Transform parent, Transform stock, bool isCorrect, string i)
    {
        this.isCorrectAnswer = isCorrect;
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.pullingStock = stock;
        isActive = true;
        
        UpdateText(i);
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
        if (isCorrectAnswer)
        {
            Debug.Log("Register Answer");
            Debug.Log(text.text);
        }
        else
        {
            Debug.Log("Give penalty");
        }
    }

}
