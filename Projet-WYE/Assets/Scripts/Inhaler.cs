using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class Inhaler : MonoBehaviour
{
    public CombinableObject _combiObject;

    public CombinableObject healthProduct;
    public CombinableObject dangerousProduct;

    public XRSocketInteractorWithAutoSetup socket;

    public UnityEvent doAction;

    private bool isComplete, wasComplete;

    public void Check()
    {
        if (socket.isMatching && socket.snapToA == healthProduct.gameObject)
        {
            isComplete = true;

            if (isComplete && !wasComplete)
            {
                isComplete = false;
                wasComplete = true;

                _combiObject.dissolveEffect.startEffect = true;
                healthProduct.dissolveEffect.startEffect = true;
                dangerousProduct.dissolveEffect.startEffect = true;

                OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[0].influence, _combiObject.useWith[0].outcome, _combiObject.useWith[0].isLethal);
                //OrderController.Instance.ResolvePuzzle();
                doAction?.Invoke();
            }
        }
        else if (socket.isMatching && socket.snapToB == dangerousProduct.gameObject)
        {
            isComplete = true;

            if (isComplete && !wasComplete)
            {
                isComplete = false;
                wasComplete = true;

                _combiObject.dissolveEffect.startEffect = true;
                healthProduct.dissolveEffect.startEffect = true;
                dangerousProduct.dissolveEffect.startEffect = true;

                OrderController.Instance.AddCombinaison(_combiObject, healthProduct, _combiObject.useWith[1].influence, _combiObject.useWith[1].outcome, _combiObject.useWith[1].isLethal);
                //OrderController.Instance.ResolvePuzzle();
                doAction?.Invoke();
            }
        }
    }
}
