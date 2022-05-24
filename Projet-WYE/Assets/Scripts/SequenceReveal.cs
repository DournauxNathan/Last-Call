using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceReveal : MonoBehaviour
{
    public Material material;

    public bool simulateInput;
    private float amount;
    public float maxAmount;
    public float speed;

    public bool isLastPos;

    // Update is called once per frame
    void Update()
    {
        if (simulateInput)
        {
            simulateInput = !simulateInput;

            GetComponent<ShakeWord>().isDecaying = true;
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
            amount += Time.deltaTime * speed;

            material.SetFloat("_Dissolve", amount);

            if (amount > maxAmount)
            {
                amount = maxAmount;

                StopAllCoroutines();

                yield return null;
            }

            yield return null;
        }
    }

}
