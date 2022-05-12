using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : MonoBehaviour
{
    private Renderer targetRenderer;
    private float timeForDecay;
    private bool isColorDefault = true;
    
    public int index;

    public List<GameObject> button;

    public Material defaultMaterial;
    public Material highlightMaterial;

    public float highlightDuration = 0.7f;

    void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;

        timeForDecay = highlightDuration;
    }

    private void Update()
    {
        Highlight();
    }

    public void Highlight()
    {
        timeForDecay -= Time.deltaTime * highlightDuration;

        if (timeForDecay <= 0 && !isColorDefault)
        {
            button[MasterManager.Instance.buttonEmissive].GetComponent<Renderer>().material = defaultMaterial;

            isColorDefault = true;
            timeForDecay = highlightDuration;
        }
        else if (timeForDecay <= 0 && isColorDefault)
        {
            button[MasterManager.Instance.buttonEmissive].GetComponent<Renderer>().material = highlightMaterial;

            isColorDefault = false;
            timeForDecay = highlightDuration;
        }
    }
}
