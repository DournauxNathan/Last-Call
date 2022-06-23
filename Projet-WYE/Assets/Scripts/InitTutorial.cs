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

    public List<GameObject> objects;
    private void Awake()
    {
        if (MasterManager.Instance.isTutoEnded)
        {
            TutoManager.Instance.Init();
            Debug.Log("Skip tuto");
        }
        else
        {
            HandController.Instance.showController = true;
            HandController.Instance.showController = true;

            TutoManager.Instance.Init();

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
    }

    public void DisableObject()
    {
        foreach (var item in objects)
        {
            item.SetActive(false);
        }
    }

    public void Progress(int i)
    {
        TutoManager.Instance.Progress(i);
    }

    public void Skip()
    {
        TutoManager.Instance.Skip();
    }

    public void ActiveGO(GameObject go)
    {
        go.SetActive(true);
    }
}
