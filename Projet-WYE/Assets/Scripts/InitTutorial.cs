using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InitTutorial : Singleton<InitTutorial>
{
    [Header("Refs")]
    public GameObject grab;
    public GameObject pointAndClick;
    public GameObject pointAndClickcomplentaire;
    public GameObject order;

    [Header("Texts")]
    public TMP_Text grabText;
    public TMP_Text pointAndClickText;
    public TMP_Text orderText;

    private void Awake()
    {
        if (grab != null)
        {
            grab.SetActive(false);
        }
        if (pointAndClick != null)
        {
            pointAndClick.SetActive(false);
        }
        if (pointAndClickcomplentaire != null)
        {
            pointAndClickcomplentaire.SetActive(false);
        }

        if (TutoManager.Instance.firstPartIsDone)
        {
            if (grab != null)
            {
                grab.SetActive(false);
            }
            if (pointAndClick != null)
            {
                pointAndClick.SetActive(false);
            }
            if (pointAndClickcomplentaire != null)
            {
                pointAndClickcomplentaire.SetActive(false);
            }
        }
    }

    public void Progress(int i)
    {
        TutoManager.Instance.Progress(i);
    }
}
