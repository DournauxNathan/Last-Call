using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LoadFromCsv 
{
    [MenuItem("Puzzles/ScriptableObjects/Generate")]
    public static void LoadCSVToSO()
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

    [MenuItem("Puzzles/Prefab/Generate")]
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
        GameObject newPrefab = new GameObject(entry[0], typeof(CombinableObject));
    }

    [MenuItem("Puzzles/Prefab/Save Current Selection")]
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


}
