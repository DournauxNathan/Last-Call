using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class Debugger : Singleton<Debugger>
{
    private void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb.numpad0Key.wasPressedThisFrame)
        {
            
            ActivateAllObjectInImaginary();
        }
        if (kb.numpad1Key.wasPressedThisFrame)
        {
            GoToImaginary();
        }
        if (kb.numpad2Key.wasPressedThisFrame)
        {
            SolveImaginary();
        }
        if (kb.numpad3Key.wasPressedThisFrame)
        {
            AnswerAllOrder();
        }
    }


    public void ActivateAllObjectInImaginary()
    {
        Debug.Log("0 is pressed,\n Activate all object");

        foreach (var item in ObjectActivator.Instance.desactivatedObject)
        {
            item.gameObject.SetActive(true);
            
        }
        OrderController.Instance.numberOfCombinaison = ObjectActivator.Instance.desactivatedObject.Count / 2; // a chnager si methode de calcule change
    }

    public void GoToImaginary()
    {
        if (!MasterManager.Instance.isInImaginary && SceneLoader.Instance.GetCurrentScene().name == "Persistent" )
        {
            SceneLoader.Instance.LoadNewScene("Office");
            Debug.Log("Office Loaded");

        }

        if (SceneLoader.Instance.GetCurrentScene().name != "Persistent" && !MasterManager.Instance.isInImaginary)
        {
            switch (ScenarioManager.Instance.currentScenario)
            {
                case Scenario.TrappedMan:
                    SceneLoader.Instance.LoadNewScene("TrappedMan");
                    break;
                case Scenario.HomeInvasion:
                    SceneLoader.Instance.LoadNewScene("HomeInvasion");
                    break;
                case Scenario.RisingWater:
                    SceneLoader.Instance.LoadNewScene("RisingWater");
                    break;
            }

            Projection.Instance.isTransition = true;
        }
        else
        {
            Debug.Log("Already in Imaginary");
        }
 
    }
    
    public void SolveImaginary()
    {
        if (MasterManager.Instance.isInImaginary)
        {
            switch (ScenarioManager.Instance.currentScenario)
            {
                case Scenario.TrappedMan:

                    //OrderController.Instance.orders.AddRange(ScenarioManager.Instance.o_trappedMan);

                    MasterManager.Instance.isInImaginary = false;
                    OrderController.Instance.isResolve = true; 
                    SceneLoader.Instance.LoadNewScene("Office");

                    break;
                case Scenario.HomeInvasion:
                    OrderController.Instance.isResolve = true;
                    //OrderController.Instance.orders.AddRange(ScenarioManager.Instance.o_homeInvasion);
                    MasterManager.Instance.isInImaginary = false;
                    SceneLoader.Instance.LoadNewScene("Office");
                    break;
                case Scenario.RisingWater:
                    OrderController.Instance.isResolve = true;
                    //OrderController.Instance.orders.AddRange(ScenarioManager.Instance.o_domesticAbuse);
                    MasterManager.Instance.isInImaginary = false;
                    SceneLoader.Instance.LoadNewScene("Office");
                    break;
            }
        }
        else
        {
            Debug.Log("Not in Imaginary");
        }

        
    }

    public void AnswerAllOrder()
    {
        InstantiableButton[] _temp = FindObjectsOfType<InstantiableButton>();
        /*foreach (var item in _temp)
        {
            item.Desactivate();
        }*/

        foreach (var order in OrderController.Instance.ordersStrings)
        {

            ScenarioManager.Instance.endingValue += order.influence ;
        }
        Debug.Log("endingValue= " + ScenarioManager.Instance.endingValue +"\n Call next Phases");
    }



}
