using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : PhysicsButton
{
    public void Press()
    {
        //push.GetComponent<BoxCollider>().enabled = false;
        
        OrderController.Instance.IncreaseValue(1);
    }
}   
