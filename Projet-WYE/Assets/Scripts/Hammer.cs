using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public CombinableObject hammer;

    public int count;
    public int maxNailsToDrive;

    private bool doOnce = true;

    public void Increase()
    {
        count += 1;

        if (count >= maxNailsToDrive)
        {
            Check();
        }
    }

    public void Check()
    {
        if (doOnce && count >= maxNailsToDrive)
        {
            doOnce = !doOnce;
            OrderController.Instance.AddOrder(hammer.useWith[0].influence, hammer.useWith[0].outcome, hammer.useWith[0].isLethal);
            OrderController.Instance.ResolvePuzzle();
        }
    }
}
