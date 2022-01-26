using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                tabs[2].SetActive(false);
                break;

            case 1:
                tabs[2].SetActive(false);
                break;

            case 2:
                tabs[0].SetActive(false);
                tabs[1].SetActive(false);
                break;
        }

    }
}
