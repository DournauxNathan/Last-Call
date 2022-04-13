using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hint", menuName = "Scenario/Hint", order = 3)]
public class Hint : ScriptableObject
{
    public List<AudioClip> hint_voiceLines;
}
