using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MasterManager : Singleton<MasterManager>
{
    [Header("Refs")]
    public XRInteractionManager xRInteractionManager;
    public ObjectActivator objectActivator;
    public AudioSource mainAudioSource;

    [Header("Metrics")]
    public bool isInImaginary;
    public bool pillsEffect;
    [Tooltip("Number of pills taken by the player")]
    public int currentPills = 0;


    void EffectOfPills()
    {
        if (currentPills > 0)
        {
            //Expand the timer of the call
            /* The time is ""slow"", the events of the call arrived less faster ? */

            //Active Interactor of useless objets
        }
        else if (currentPills > 1)
        {
            /*Active Interactor of useless
             *Actvate their outline
             */
        }
    }
}
