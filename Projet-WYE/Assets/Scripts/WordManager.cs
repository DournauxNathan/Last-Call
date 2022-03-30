using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : Singleton<WordManager>
{
    public SphereCollider spawner;
    public Transform getTransfrom;
    public Transform pullingStock;
    public List<Answer> answers;
    public List<WordData> canvasWithWordData;
        
    public void PullWord()
    {
        //Get all answer
        foreach (Answer answer in answers)
        {
            //Get the keywords in the answer
            for (int i = 0; i < answer.keywords.Length; i++)
            {
                //Find any available Canvas Word 
                var item = FindAvailableItem();
                //if true, Activate Canvas Word and Set his text with the current propo
                item.Activate(transform, pullingStock, answer.keywords[i].isCorrectAnswer, answer.keywords[i].proposition);
            }
        }
    }

    public WordData FindAvailableItem()
    {
        //Get all WprdData
        foreach (var item in canvasWithWordData)
        {
            if(!item.IsActive)
            {
                return item;
            }
        }
        return null;
    }
}
