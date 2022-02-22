using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class PlaytestData : Singleton<PlaytestData>
{    
    public Player betaTesteurs = new Player();
           
    public void SaveIntoJson()
    {
        string player = JsonUtility.ToJson(betaTesteurs);

        string localPath = "Assets/Playtests/" + betaTesteurs.player + ".json";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        //localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        //localPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        localPath = @"D:\TestJson\"; //Debug.Log(localPath);
        System.IO.File.WriteAllText(localPath + betaTesteurs.player +".json", player);
    }

    public string ConvertTime()
    {
        //Calculate the time in minutes and seconds.
        int minutes = (int)betaTesteurs.data.timerCall / 60;
        int seconds = (int)betaTesteurs.data.timerCall % 60;

        //Update the duration text.
        return betaTesteurs.data.timeOfTheCall = minutes.ToString() + ":" + ((seconds < 10) ? ("0") : ("")) + seconds.ToString();
    }

    public void ReportBug()
    {
        Array.Resize(ref betaTesteurs.data.bugsReport, betaTesteurs.data.bugsReport.Length + 1);

        betaTesteurs.data.bugsReport[betaTesteurs.data.bugsReport.Length - 1].atPhase = MasterManager.Instance.currentPhase;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(PlaytestData))]
public class PlaytestEditor : Editor
{
    PlaytestData script;
    public override void OnInspectorGUI()
    {
        script = target as PlaytestData;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            script.ConvertTime();
            script.SaveIntoJson();
        }
    }
    
}
#endif
[System.Serializable]
public class DataFromScenario
{
    public Scenario scenario;

    public string timeOfTheCall = PlaytestData.Instance.ConvertTime();
     public float timerCall; 

    public List<string> answeredQuestions;
    public List<Unit> unitSended;

    public int numberOfCombinaisonsMade;

    public Bug[] bugsReport;
}

[System.Serializable]
public class Player
{
    public string player;
    public DataFromScenario data;
}

public class Bug
{
    public Phases atPhase;
}

