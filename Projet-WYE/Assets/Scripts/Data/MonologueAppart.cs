using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New monologue", menuName = "Scenario/Monologue", order = 2)]
public class MonologueAppart : ScriptableObject
{
    public AudioClip monologueClip;
    public string text;
    public Scenario scenario;
    [Range(-1,1)]public int value;
}
