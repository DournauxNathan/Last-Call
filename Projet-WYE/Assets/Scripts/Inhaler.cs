using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inhaler : MonoBehaviour
{
    public CombinableObject healthProduct;
    public CombinableObject dangerousProduct;

    public XRSocketInteractorWithAutoSetup socket;

    public CombinableObject _combiObject;

    public void Check()
    {
        Debug.Log(socket.isMatching);
        Debug.Log(socket.snapToA == healthProduct);
        
        Debug.Log(socket.isMatching && socket.snapToA == healthProduct);

        if (socket.isMatching && socket.snapToA == healthProduct)
        {
            OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[0].influence, _combiObject.useWith[0].outcome, _combiObject.useWith[0].isLethal);
            healthProduct = null;
        }
        else if (socket.isMatching && socket.snapToB == dangerousProduct)
        {
            OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[1].influence, _combiObject.useWith[1].outcome, _combiObject.useWith[1].isLethal);
            healthProduct = null;
        }
    }
}
