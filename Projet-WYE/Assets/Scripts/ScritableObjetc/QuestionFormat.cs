using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 1)]
public class QuestionFormat : ScriptableObject
{
    public string[] listeDeQuestion;
    public int currentClick = 0;
    public GameObject[] listeLieObjet;
    public AudioClip[] voiceLine;
    
}