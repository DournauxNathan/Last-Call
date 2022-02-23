using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTabSelection : Singleton<UiTabSelection>
{
    public List<GameObject> tabs;
    public int indexTab = 0;

    public List<GameObject> unitDispatcherFeedbacks;

    public void SwitchTab(int i)
    {
        tabs[i].SetActive(true);

        switch (i)
        {
            case 0:
                tabs[1].SetActive(false);
                EventSystem.current.SetSelectedGameObject(UIManager.Instance.checkListTransform.GetChild(0).GetComponentInChildren<Button>().gameObject);
                tabs[2].SetActive(false);
                break;

            case 1:
                tabs[2].SetActive(false); 
                EventSystem.current.SetSelectedGameObject(UIManager.Instance.descriptionTransform.GetChild(0).GetComponentInChildren<Button>().gameObject);
                break;

            case 2:
                tabs[0].SetActive(false);
                tabs[1].SetActive(false);
                break;
        }
    }

    public void UpdateIndex(int i)
    {
        indexTab = i;
    }

    public void SwitchSequence(int i)
    {
        switch (i)
        {
            case 1:
                unitDispatcherFeedbacks[0].SetActive(true);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 2:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(true);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 3:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(true);
                unitDispatcherFeedbacks[3].SetActive(false);
                break;

            case 4:
                unitDispatcherFeedbacks[0].SetActive(false);
                unitDispatcherFeedbacks[1].SetActive(false);
                unitDispatcherFeedbacks[2].SetActive(false);
                unitDispatcherFeedbacks[3].SetActive(true);
                break;
        }
    }
}
