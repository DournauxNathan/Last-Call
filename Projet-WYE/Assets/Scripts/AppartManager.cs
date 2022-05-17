using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppartManager : Singleton<AppartManager>
{
    //SceneLoader.Instance.LoadNewScene("");
    public string currentAppart = "";
        
    public void LoadAppartOnScenarioEnd() //Load the next appart
    {
        switch (ScenarioManager.Instance.currentScenario)
        {
            case Scenario.TrappedMan:
                Debug.Log("1");
                MasterManager.Instance.ChangeSceneByName(4, "Appartment_Day 0");
                //InitializeAppart(Scenario.HomeInvasion,"Appartment_Day 0");
                break;
            case Scenario.HomeInvasion:
                Debug.Log("2");
                if (ScenarioManager.Instance.endingValue<0)
                    MasterManager.Instance.ChangeSceneByName(4, "Appartment_Day-1");
                // InitializeAppart(Scenario.RisingWater,"Appartment_Day-1");

                if (ScenarioManager.Instance.endingValue>0)
                    MasterManager.Instance.ChangeSceneByName(4, "Appartment_Day+1");
                //InitializeAppart(Scenario.RisingWater,"Appartment_Day+1");

                break;
            case Scenario.RisingWater:
                Debug.Log("3");
                if (ScenarioManager.Instance.endingValue<0)
                    MasterManager.Instance.ChangeSceneByName(4, "Appartment_Day-2");
                // InitializeAppart(Scenario.None,"Appartment_Day-2");
                if (ScenarioManager.Instance.endingValue>0)
                    MasterManager.Instance.ChangeSceneByName(4, "Appartment_Day+2");
                //  InitializeAppart(Scenario.None,"Appartment_Day+2");
                break;
        }
    }

    public void LoadAppartOnScenarioEnd(Scenario scenario) //Can load a specific appart
    {
        switch (scenario)
        {
            case Scenario.TrappedMan:

                InitializeAppart(Scenario.HomeInvasion,"Appartment_Day 0");

                break;
            case Scenario.HomeInvasion:
                if(ScenarioManager.Instance.endingValue<0){InitializeAppart(Scenario.RisingWater,"Appartment_Day-1");}
                if(ScenarioManager.Instance.endingValue>0){InitializeAppart(Scenario.RisingWater,"Appartment_Day+1");}

                break;
            case Scenario.RisingWater:
                if(ScenarioManager.Instance.endingValue<0){InitializeAppart(Scenario.None,"Appartment_Day-2");}
                if(ScenarioManager.Instance.endingValue>0){InitializeAppart(Scenario.None,"Appartment_Day+2");}

                break;
        }
    }

    public void recoverAppartFromSave()
    {
        if(MasterManager.Instance.TryGetComponent<InputHandler>(out InputHandler inputHandler))
        {
            inputHandler.LoadSave();
            currentAppart = inputHandler.GetCurrentAppart();
            SceneManager.LoadScene(currentAppart);
        }
        
        
    }

    private void InitializeAppart(Scenario scenario, string _appart)
    {
        ChangeScenario(scenario);
        ChangeCurrentAppart(_appart);
        MasterManager.Instance.ChangeSceneByName(4, _appart);
    }

    private void ChangeScenario(Scenario scenario)
    {
        ScenarioManager.Instance.currentScenario = scenario;
    }
    private void ChangeCurrentAppart(string _appart)
    {
        currentAppart = _appart;
    }

    public void LoadDefaultAppart()
    {
        SceneLoader.Instance.LoadNewScene("Appartment_Day 0");
        currentAppart = "Appartment_Day 0";
    }
}
