using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class QuestionFormat : ScriptableObject
{
    public int currentClick = 0;
    [Space(10)]
    public string[] listQuestion;
    public AudioClip[] voiceLine;
    [Tooltip("X = id ; Y = number of click")]
    public Vector2[] listIdObject;    
}