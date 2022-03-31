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
    public int atIndex;

    private bool isActive;
    public bool IsActive => isActive;

    public void Activate(Transform parent, Transform stock, Question _question, string i, int _index)
    {
        this.parentTransform = parent;
        transform.SetParent(parent);

        this.question = _question;
        this.atIndex = _index;

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
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        while (true)
        {
            amount += Time.deltaTime;

            foreach (var item in question.question[atIndex].linkObjects)
            {
                item.SetFloat("_Dissolve", amount);
            }

            if (amount >= 15f)
            {
                amount = 15f;

                StopCoroutine(Show());
            }

            yield return null;
        }
    }
}