using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTabSelection : Singleton<UiTabSelection>
{
    public List<GameObject> tabs;
    public int indexTab = 0;
    public bool switchTab;
    public List<GameObject> unitDispatcherFeedbacks;

    private void Update()
    {
        if (switchTab)
        {
            switchTab = false;
            SwitchTab(indexTab);
        }
    }

    public void SwitchTab(int i)
    {
        tabs[i].SetActive(true);

        switch (i)
        {
            case 0:
                //tabs[0].SetActive(true);
                tabs[1].SetActive(false);
                tabs[2].SetActive(false);

                UIManager.Instance.UpdateEventSystem(UIManager.Instance.checkListTransform);

                break;

            case 1:
                tabs[1].SetActive(true);
                tabs[2].SetActive(false);

                UIManager.Instance.UpdateEventSystem(UIManager.Instance.descriptionTransform);
                break;

            case 2:
                tabs[0].SetActive(false);
                tabs[1].SetActive(false);
                tabs[2].SetActive(true);
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
