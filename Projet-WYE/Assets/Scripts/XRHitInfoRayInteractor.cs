using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHitInfoRayInteractor : XRRayInteractor
{
    public List<GameObject> interactors;
    private InputHelpers.Button usage;

    public void GetHoverInteractors()
    {
        Debug.Log("enterHoverDetection taille liste: " + interactors.Count + 
            "\n ValidTarget size: " + validTargets.Count);
        
        if (interactors.Count == 0)
        {
            interactors.Add(validTargets[0].gameObject);
        }

        for (int i = 0; i < interactors.Count; i++)
        {
            if (!interactors.Contains(validTargets[0].gameObject))
            {
                interactors.Add(validTargets[0].gameObject);
            }

            /*if (interactors[i].gameObject.name != validTargets[0].gameObject.name) //Check seulement le premier element donc -> 1/boule 2/capsule si reste sur capsule ajoute a l'infinit
            {
                interactors.Add(validTargets[0].gameObject);
            }*/

            else
            {
                Debug.LogWarning("There already this object in the interactors list");
            }
        }
    }

    private void Update()
    {
        switch (usage)
        {
            #region - Trigger -
            case InputHelpers.Button.Trigger:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
                {

                }
                break;

            case InputHelpers.Button.TriggerButton:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButtonState))
                {

                }
                break;
            #endregion

            #region - Grip -
            case InputHelpers.Button.Grip:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
                {

                }
                break;

            case InputHelpers.Button.GripButton:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.grip, out bool gripButtonState))
                {

                }
                break;
            #endregion

            #region - Primary Button -
            case InputHelpers.Button.PrimaryButton:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonState))
                {

                }
                break;

            case InputHelpers.Button.PrimaryTouch:
                break;
            #endregion

            #region - Secondary Button -
            case InputHelpers.Button.SecondaryButton:

                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonState))
                {

                }
                break;

            case InputHelpers.Button.SecondaryTouch:
                break;
            #endregion

            #region - Primary Axis -
            case InputHelpers.Button.Primary2DAxisTouch:
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool primaryAxisTouchButtonState))
                {

                }
                break;

            case InputHelpers.Button.Primary2DAxisClick:
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool primaryAxisClickButtonState))
                {

                }
                break;
            #endregion

            #region - Secondary Axis -
            case InputHelpers.Button.Secondary2DAxisTouch:
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out bool secondaryAxisTouchButtonState))
                {

                }
                break;

            case InputHelpers.Button.Secondary2DAxisClick:
                if (HandPresence.instance.targetDevice.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool secondaryAxisClickButtonState))
                {

                }
                break;
            #endregion

            #region - Primary Axis 2D -
            case InputHelpers.Button.PrimaryAxis2DUp:
                break;
            case InputHelpers.Button.PrimaryAxis2DDown:
                break;
            case InputHelpers.Button.PrimaryAxis2DLeft:
                break;
            case InputHelpers.Button.PrimaryAxis2DRight:
                break;
            #endregion

            #region - Secondary Axis 2D -
            case InputHelpers.Button.SecondaryAxis2DUp:
                break;
            case InputHelpers.Button.SecondaryAxis2DDown:
                break;
            case InputHelpers.Button.SecondaryAxis2DLeft:
                break;
            case InputHelpers.Button.SecondaryAxis2DRight:
                break;
            #endregion        
        }
    }

}
