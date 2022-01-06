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
        if (currentPills == 1)
        {
            //Expand the timer of the call
            /* The time is ""slow"", the events of the call arrived less faster ? */

            //Active  Interactor, outline of useless objets
            objectActivator.ToggleUselessObject(true, 3);
        }
        else if (currentPills > 1)
        {
            objectActivator.ToggleUselessObject(true, 3);
        }
    }
}
