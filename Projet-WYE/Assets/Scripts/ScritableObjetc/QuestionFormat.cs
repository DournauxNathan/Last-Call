using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class QuestionFormat : ScriptableObject
{
    public string[] listeDeQuestion;
    public int currentClick = 0;
    public AudioClip[] voiceLine;
    [Tooltip("x = id ; y = nombre de clic")]
    public Vector2[] listIdObject;
    
}