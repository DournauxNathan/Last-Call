using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : MonoBehaviour
{
    private Color defaultColor;
    private Renderer targetRenderer;
    private float timeForDecay;
    private bool isColorDefault = true;
    [Header("Parameters")]
    public GameObject target;
    public Color highlightedColor;
    public float highlightDuration = 0.7f;

    void Start()
    {
        targetRenderer = target.GetComponent<Renderer>();
        defaultColor = targetRenderer.material.GetColor("_BASE_COLOR");
        timeForDecay = highlightDuration;
    }

    private void Update() {
        timeForDecay -= Time.deltaTime * highlightDuration;
        if (timeForDecay <= 0 && !isColorDefault) {
            targetRenderer.material.SetColor("_BASE_COLOR", defaultColor);
            isColorDefault = true;
            timeForDecay = highlightDuration;
        }
        else if (timeForDecay <= 0 && isColorDefault) {
            targetRenderer.material.SetColor("_BASE_COLOR", highlightedColor);
            isColorDefault = false;
            timeForDecay = highlightDuration;
        }
    }
}
