using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hints", menuName = "Hint", order = 1)]
public class Hint : ScriptableObject
{
    public List<AudioClip> hint_voiceLines;
}
