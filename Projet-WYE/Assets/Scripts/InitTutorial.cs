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
        grab.SetActive(false);
        pointAndClick.SetActive(false);
        pointAndClickcomplentaire.SetActive(false);
    }

    public void Progress(int i)
    {
        TutoManager.Instance.Progress(i);
    }
}
