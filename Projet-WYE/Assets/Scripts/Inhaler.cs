using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Inhaler : MonoBehaviour
{
    public CombinableObject _combiObject;

    public CombinableObject healthProduct;
    public CombinableObject dangerousProduct;

    public XRSocketInteractorWithAutoSetup socket;


    private bool doOnce = true;

    public void Check()
    {
        if (socket.isMatching && socket.snapToA == healthProduct.gameObject && doOnce)
        {
            doOnce = !doOnce;
            _combiObject.dissolveEffect.startEffect = true;
            healthProduct.dissolveEffect.startEffect = true;
            dangerousProduct.dissolveEffect.startEffect = true;

            OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[0].influence, _combiObject.useWith[0].outcome, _combiObject.useWith[0].isLethal);
            
        }
        else if (socket.isMatching && socket.snapToB == dangerousProduct.gameObject && doOnce)
        {
            doOnce = !doOnce;
            _combiObject.dissolveEffect.startEffect = true;
            healthProduct.dissolveEffect.startEffect = true;
            dangerousProduct.dissolveEffect.startEffect = true;

            OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[1].influence, _combiObject.useWith[1].outcome, _combiObject.useWith[1].isLethal);

        }
    }
}
