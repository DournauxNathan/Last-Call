using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Padlock : MonoBehaviour
{
    [Header("Refs")]
    public CombinableObject data;

    [Header("Propeties")]
    public int[] correctCombination, currentCombination;
    public bool isComplete;
    public List<RotateWheel> wheels;

    private void Start()
    {
        currentCombination = new int[] { wheels[0].currentFace, wheels[1].currentFace, wheels[2].currentFace, wheels[3].currentFace };
        RotateWheel.Rotated += CheckResult;
    }

    private void CheckResult(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "wheel_a":
                currentCombination[0] = number;
                break;
            case "wheel_b":
                currentCombination[1] = number;
                break;
            case "wheel_c":
                currentCombination[2] = number;
                break;
            case "wheel_d":
                currentCombination[3] = number;
                break;
        }

        if ((currentCombination[0] == correctCombination[0]) 
            && (currentCombination[1] == correctCombination[1]) 
            && (currentCombination[2] == correctCombination[2])
            && (currentCombination[3] == correctCombination[3]))
        {
            OrderController.Instance.AddOrder(data.useWith[0].influence, data.useWith[0].outcome, data.useWith[0].isLethal);
        }
    }

    private void OnDestroy()
    {
        RotateWheel.Rotated -= CheckResult;
    }
}
