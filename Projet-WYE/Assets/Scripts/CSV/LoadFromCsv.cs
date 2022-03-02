using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;

public class LoadFromCsv 
{
    #if UNITY_EDITOR
    [MenuItem("Rational/Puzzles/ScriptableObjects/Generate")]
    public static void LoadCSVToScriptableObjects()
    {
        var csvText = Resources.Load<TextAsset>("Puzzle_Rational/Test").text;

        string[] lineSeparators = new string[] { "\n", "\r", "\n\r", "\r\n" };
        char[] cellSeparator = new char[] { ';' };

        var lines = csvText.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);
        List<string[]> completeExcelFile = new List<string[]>();

        foreach (var i in lines)
        {
            completeExcelFile.Add(i.Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries));           
        }

        foreach (var item in completeExcelFile.Skip(1))
        {
            CreateScriptable(item);
        }
    }

    [MenuItem("Rational/Puzzles/Prefab/Generate")]
    public static void LoadCSVToPrefab()
    {
        var csvText = Resources.Load<TextAsset>("Puzzle_Rational/Test").text;

        string[] lineSeparators = new string[] { "\n", "\r", "\n\r", "\r\n" };
        char[] cellSeparator = new char[] { ';' };

        var lines = csvText.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);
        List<string[]> completeExcelFile = new List<string[]>();

        foreach (var i in lines)
        {
            completeExcelFile.Add(i.Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries));
        }

        foreach (var item in completeExcelFile.Skip(1))
        {
            CreatePrefab(item);
        }

    }
    #endif
    private static void CreateScriptable(string[] entry)
    {
        /*var newScriptableObject = ScriptableObject.CreateInstance<CombinableObject_Data>();
        newScriptableObject.Init(entry);
        
        // Set the path as within the Assets folder,
        // and name it as the ScriptableObject's name with the .Asset format
        string localPath = "Assets/Scripts/CSV/" + newScriptableObject.Name + ".asset";

        AssetDatabase.CreateAsset(newScriptableObject, localPath);*/
    }

    static void CreatePrefab(string[] entry)
    {
        GameObject newPrefab = GameObject.Find(entry[0]);
        if(newPrefab == null)
        {
            newPrefab =  new GameObject(entry[0]);
        }
        var co = newPrefab.GetComponent<CombinableObject>();
        if (co == null)
        {
            co = newPrefab.AddComponent<CombinableObject>();
        }
        co.GetComponent();


        if(!co.MeshFilter)
        {
            co.MeshFilter = newPrefab.AddComponent<MeshFilter>();
        }
        if (!co.MeshRenderer)
        {
            co.MeshRenderer = newPrefab.AddComponent<MeshRenderer>();
        }
        if (!co.MeshCollider)
        {
            co.MeshCollider = newPrefab.AddComponent<MeshCollider>();
        }
        if (!co.SphereCollider)
        {
            co.SphereCollider = newPrefab.AddComponent<SphereCollider>();
        }
        if (!co.outline)
        {
            co.outline = newPrefab.AddComponent<Outline>();
        }
        

        if (entry[2].Contains("DYNAMIQUE"))
        {
            XRGrabInteractableWithAutoSetup xrInteractable = newPrefab.GetComponent<XRGrabInteractableWithAutoSetup>();
            if (xrInteractable == null)
            {
                xrInteractable= newPrefab.AddComponent<XRGrabInteractableWithAutoSetup>();
            }
            var mustImplementToogleOutline = true;
            for (int i = 0; i < xrInteractable.hoverEntered.GetPersistentEventCount(); i++)
            {
                if(xrInteractable.hoverEntered.GetPersistentMethodName(i) == "ToggleOutline")
                {
                    mustImplementToogleOutline = false;
                }
            }
            if(mustImplementToogleOutline)
            {
                UnityAction<bool> action = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action,true);
            }
        }
        else if (entry[2].Contains("STATIQUE"))
        {
            XRSimpleInteractableWithAutoSetup xrInteractable = newPrefab.GetComponent<XRSimpleInteractableWithAutoSetup>();
            if (xrInteractable == null)
            {
                xrInteractable = newPrefab.AddComponent<XRSimpleInteractableWithAutoSetup>();
            }
            var mustImplementToogleOutline = true;
            for (int i = 0; i < xrInteractable.hoverEntered.GetPersistentEventCount(); i++)
            {
                if (xrInteractable.hoverEntered.GetPersistentMethodName(i) == "ToggleOutline")
                {
                    mustImplementToogleOutline = false;
                }
            }
            if (mustImplementToogleOutline)
            {
                UnityAction<bool> action = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action, true);
            }
        }

        co.Init(entry);
    }
    #if UNITY_EDITOR
    [MenuItem("Rational/Puzzles/Prefab/Save Current Selection")]
    static void SaveCurrentSelectionIntoPrefabAsset()
    {
        if (Selection.gameObjects != null && !EditorUtility.IsPersistent(Selection.activeGameObject))
        {
            foreach (var go in Selection.gameObjects)
            {
                string localPath = "Assets/Prefabs/Combinables/" + go.name + ".prefab";

                // Make sure the file name is unique, in case an existing Prefab has the same name.
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                // Create the new Prefab.
                PrefabUtility.SaveAsPrefabAssetAndConnect(go, localPath, InteractionMode.UserAction);
            }
        }
        else
        {
            Debug.LogWarning("Selection is null");
        }
    }

    [MenuItem("Rational/Excel/Generate ScriptableObject")]
    public static void LoadCSV()
    {
        
    }
    #endif
}
