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

    private float x, y, z;
    Vector3 pos;

    private QuestionData currentQuestion => question.questions[atIndex];

    public bool simulateInput;

    private void Update()
    {
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
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        while (true)
        {
            amount += Time.deltaTime * Projection.Instance.time;

            foreach (var item in question.questions[atIndex].linkObjects)
            {
                item.SetFloat("_Dissolve", amount);
            }

            if (amount > 30f)
            {
                amount = 30f;

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