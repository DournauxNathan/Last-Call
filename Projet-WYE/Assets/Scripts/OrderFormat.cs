using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Orders", menuName = "Order", order = 2)]
public class OrderFormat : ScriptableObject
{
    public int endingModifier;
    public string orderText;
    public AudioClip[] voiceLines;
}
