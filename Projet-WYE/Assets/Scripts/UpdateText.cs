using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateTextInfo(string s)
    {
        text.text = "Phase (current " + s+ ")";
    }

    public void UpdateTextInfo(int i)
    {
        text.text = i.ToString();
    }
}
