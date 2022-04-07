using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRBinder : MonoBehaviour
{
    public void OnSecondary()
    {
        Projection.Instance.isTransition = true;
    }

    public void OnSecondaryEnd()
    {
        Projection.Instance.isTransition = false;
        Projection.Instance.ResetTransition();
    }

    public void OnAxisClick()
    {
        if (MasterManager.Instance.currentPhase == Phases.Phase_1 && MasterManager.Instance.currentPhase == Phases.Phase_3)
        {
            int i = UiTabSelection.Instance.indexTab++;

            if (i >= 3)
            {
                UiTabSelection.Instance.indexTab = 0;
            }

            UiTabSelection.Instance.UpdateIndex(i);
            //UiTabSelection.Instance.SwitchTab(i);
        }
    }

    public void OnMenuButton()
    {
        if (MasterManager.Instance.currentPhase != Phases.Phase_0 && MasterManager.Instance.currentPhase != Phases.Phase_4)
        {
            UiPauseManager.Instance.PauseDisplay();

        }
    }
}
