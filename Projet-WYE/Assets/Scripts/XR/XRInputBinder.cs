using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRInputBinder : MonoBehaviour
{

    public void OnTrigger()
    {
        
    }
    public void OnGrib()
    {

    }
    public void OnPrimaryButton()
    {

    }
    public void OnPrimaryTouch()
    {

    }
    public void OnSecondaryButton()
    {
        Projection.Instance.isTransition = true;
    }

    public void OnSecondaryButtonEnd()
    {
        Projection.Instance.isTransition = false;
        Projection.Instance.ResetTransition();
    }

    public void OnSecondaryTouch()
    {
        
    }

    public void OnMenuButton()
    {
        if (MasterManager.Instance.currentPhase != Phases.Phase_0 && MasterManager.Instance.currentPhase != Phases.Phase_4)
        {
            UiPauseManager.Instance.PauseDisplay();
        }
    }
    public void OnPrimaryAxis2D()
    {

    }
}
