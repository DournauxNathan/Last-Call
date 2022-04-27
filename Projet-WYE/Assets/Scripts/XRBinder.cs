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

    }

    public void OnMenuButton()
    {
        if (MasterManager.Instance.currentPhase != Phases.Phase_0 && MasterManager.Instance.currentPhase != Phases.Phase_4)
        {
            UiPauseManager.Instance.PauseDisplay();

        }
    }
}
