using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Protocol", menuName = "Protocol", order = 1)]
public class ProtocolFormat : ScriptableObject
{
    public string protocolAnswer;
    public AudioClip protocolAnswerAudio;
}
