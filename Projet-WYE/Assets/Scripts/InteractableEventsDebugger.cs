using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InteractableEventsDebugger : MonoBehaviour
{
    #region First/Last Hover
    public void FirstHover()
    {
        Debug.Log("First Hover");
    }

    public void LastHover()
    {
        Debug.Log("Last Hover");
    }
    #endregion

    #region Hover
    public void HoverEntered()
    {
        if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool boolValue))
        {
            Debug.Log("Save current object hit");
        }

        Debug.Log("Hover Entered");
    }

    public void HoverExited()
    {
        Debug.Log("Hover Exit");
    }
    #endregion

    #region Select
    public void SelectEnter()
    {
        Debug.Log("Select Enter");
    }
    public void SelectExited()
    {
        Debug.Log("Select Exit");
    }
    #endregion

    #region Activate
    public void Activated()
    {
        Debug.Log("Activate");
    }

    public void Deactivated()
    {
        Debug.Log("Deactivate");
    }
    #endregion
}
