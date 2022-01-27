using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTabSelection : MonoBehaviour
{
    public List<GameObject> tabs;

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
}
