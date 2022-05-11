using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : PhysicsButton
{
    public void Press()
    {
        buttonTop.GetComponent<BoxCollider>().enabled = false;
        isActivate = false;

        OrderController.Instance.ResolvePuzzle();
    }
}   
