using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHitInfoRayInteractor : XRRayInteractor
{
    public ListManager interactors;


    public void GetHoverInteractors()
    {
        if (interactors.HoveredInteractors.Count == 0)
        {
            interactors.HoveredInteractors.Add(validTargets[0].gameObject);
        }

        if (!interactors.HoveredInteractors.Contains(validTargets[0].gameObject))
        {
            interactors.HoveredInteractors.Add(validTargets[0].gameObject);
        }

        else
        {
            Debug.LogWarning("There already this object in the interactors list");
        }
    }

    public void ClearList()
    {
        interactors.ClearList();
    }

    /*private void Update()
    {
            #region - Trigger -            
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
                {

                }
                    
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonState))
                {

                }

            #endregion

            #region - Grip -

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                {

                }

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonState))
                {

                }
            #endregion

            #region - Primary Button -
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonState))
                {
                    Debug.Log("primaire");
                }
           
            #endregion

            #region - Secondary Button -
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonState))
                {
                    Debug.Log("secondaire");
                }
            #endregion

            #region - Primary Axis -
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool primaryAxisTouchButtonState))
                {

                }

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool primaryAxisClickButtonState))
                {

                }
            #endregion

            #region - Secondary Axis -
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out bool secondaryAxisTouchButtonState))
                {

                }

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool secondaryAxisClickButtonState))
                {

                }
            #endregion

    }*/
}
