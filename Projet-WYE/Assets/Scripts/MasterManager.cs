using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MasterManager : Singleton<MasterManager>
{
    public XRInteractionManager xRInteractionManager;
    public bool isInImaginary;
    public ObjectActivator objectActivator;
}
