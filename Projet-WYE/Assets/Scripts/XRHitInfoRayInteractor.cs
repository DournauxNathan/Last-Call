using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHitInfoRayInteractor : XRRayInteractor
{
    public List<GameObject> interactors;

    public void GetHoverInteractors()
    {
        Debug.Log("enterHoverDetection taille liste: " + interactors.Count + "\n ValidTarget size: " + validTargets.Count);
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

}
