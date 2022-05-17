using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : PhysicsButton
{
    public void Press()
    {
        isActivate = false;

        OrderController.Instance.AddOrder(GetComponent<CombinableObject>().useWith[0].influence, GetComponent<CombinableObject>().useWith[0].outcome, GetComponent<CombinableObject>().useWith[0].isLethal);

        OrderController.Instance.ResolvePuzzle();
    }
}   
