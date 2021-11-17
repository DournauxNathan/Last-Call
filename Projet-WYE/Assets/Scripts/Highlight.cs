using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
[RequireComponent(typeof(Outline))]
public class Highlight : MonoBehaviour
{
    public Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline.GetComponent<Outline>().enabled = false;
    }

    public void Enable()
    {
        outline.enabled = true;
    }

    public void Disabled()
    {
        outline.enabled = false;
    }
}
