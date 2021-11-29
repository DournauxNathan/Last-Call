using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class QuestionFormat : ScriptableObject
{
    public int currentClick = 0;
    public string[] listQuestion;
    public AudioClip[] voiceLine;
    [Tooltip("X = id ; Y = number of click")]
    public Vector2Int[] listIdObject;
    public List<Unit> units;
}