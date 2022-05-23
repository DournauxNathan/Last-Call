using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : Singleton<ColorIndicator>
{
    private Renderer targetRenderer;
    private float timeForDecay;
    private bool isColorDefault = true;

    [Range(0f, 0.085f)]
    public float range;
    public int index;

    public List<GameObject> buttons;

    public Material defaultMaterial;
    public Material highlightMaterial;

    public float highlightDuration = 0.7f;

    public bool indicateButton;

    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;

        timeForDecay = highlightDuration;
    }

    private void LateUpdate()
    {
        if (indicateButton)
        {
            Highlight();
        }
        /*if (indicateButton)
        {
            Highlight();
        }
        else
        {
            foreach (var item in button)
            {
                item.GetComponent<Renderer>().material = defaultMaterial;
            }
        }*/
    }

    public void Highlight()
    {
        timeForDecay -= Time.deltaTime * highlightDuration;

        if (timeForDecay <= range && !isColorDefault)
        {
            buttons[MasterManager.Instance.buttonEmissive - 1].GetComponent<Renderer>().material = defaultMaterial;
            buttons[MasterManager.Instance.buttonEmissive].GetComponent<Renderer>().material = defaultMaterial;

            isColorDefault = true;
            timeForDecay = highlightDuration;
        }
        else if (timeForDecay <= range && isColorDefault)
        {
            buttons[MasterManager.Instance.buttonEmissive].GetComponent<Renderer>().material = highlightMaterial;

            isColorDefault = false;
            timeForDecay = highlightDuration;
        }

        ResetIndication(MasterManager.Instance.buttonEmissive);
    }

    public void ResetIndication(int value)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == value)
            {
                continue;
            }
            buttons[i].GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
