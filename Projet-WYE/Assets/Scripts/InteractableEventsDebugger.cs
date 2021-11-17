using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEventsDebugger : MonoBehaviour
{
    #region First/Last Hover
    public void FirstHover()
    {
        Debug.Log("Activate current holding object");
    }

    public void LastHover()
    {
        Debug.Log("Activate current holding object");
    }
    #endregion

    #region Hover
    public void HoverEntered()
    {
        Debug.Log("Activate current holding object");
    }

    public void HoverExited()
    {
        Debug.Log("Activate current holding object");
    }
    #endregion

    #region Select
    public void SelectEnter()
    {
        Debug.Log("Activate current holding object");
    }
    public void SelectExited()
    {
        Debug.Log("Activate current holding object");
    }
    #endregion

    #region Activate
    public void Activated()
    {
        Debug.Log("Activate current holding object");
    }

    public void Deactivated()
    {
        Debug.Log("Activate current holding object");
    }
    #endregion
}
