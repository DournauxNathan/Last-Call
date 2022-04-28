using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
[System.Serializable]

public class LoadFromCsv 
{
    #if UNITY_EDITOR
    //[MenuItem("Rational/Puzzles/ScriptableObjects/Generate")]
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

    [MenuItem("Rational/Generate")]
    public static void LoadCSVToPrefab()
    {
        Object[] files = Resources.LoadAll("Puzzle_Rational");
        int nFiles = files.Length;

        for (int i = 0; i < nFiles; i++)
        {
            //var csvText = Resources.Load<TextAsset>("Puzzle_Rational/SC_#1").text;
        }

        var csvText = Resources.Load<TextAsset>("Puzzle_Rational/SC_#" + 1).text;

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
        if (entry[0].Contains("true"))
        {
            GameObject newPrefab = GameObject.Find(entry[1]);

            if (newPrefab == null)
            {
                newPrefab = new GameObject(entry[1]);
            }

            var co = newPrefab.GetComponent<CombinableObject>();
            
            //System.Array.Clear(co.useWith, 0, co.useWith.Length);

            if (co == null)
            {
                co = newPrefab.AddComponent<CombinableObject>();
            }
            co.GetComponent();

            if (!co.meshFilter)
            {
                co.meshFilter = newPrefab.AddComponent<MeshFilter>();
            }
            if (!co.meshRenderer)
            {
                co.meshRenderer = newPrefab.AddComponent<MeshRenderer>();
            }
            if (!co.meshCollider)
            {
                co.meshCollider = newPrefab.AddComponent<MeshCollider>();
            }
            if (!co.sphereCollider)
            {
                co.sphereCollider = newPrefab.AddComponent<SphereCollider>();
            }
            if (!co.outline)
            {
                co.outline = newPrefab.AddComponent<Outline>();
            }
            if (!co.dissolveEffect)
            {
                co.dissolveEffect = newPrefab.AddComponent<DissolveEffect>();
                co.dissolveEffect.Init();
            }

            if (entry[3].Contains("DYNAMIQUE"))
            {

                XRGrabInteractableWithAutoSetup xrInteractable = newPrefab.GetComponent<XRGrabInteractableWithAutoSetup>();

                if (xrInteractable == null)
                {
                    xrInteractable = newPrefab.AddComponent<XRGrabInteractableWithAutoSetup>();
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
                    UnityAction<bool> action1 = new UnityAction<bool>(co.ToggleOutline);
                    UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action1, true);

                    UnityAction<bool> action2 = new UnityAction<bool>(co.ToggleOutline);
                    UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverExited, action2, false);
                }
            }
            else if (entry[3].Contains("STATIQUE"))
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
                    UnityAction<bool> action1 = new UnityAction<bool>(co.ToggleOutline);
                    UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action1, true);
                    UnityAction<bool> action2 = new UnityAction<bool>(co.ToggleOutline);
                    UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action2, false);
                }
            }

            co.Init(entry);
        }
    }
#endif

#if UNITY_EDITOR
    //[MenuItem("Rational/Puzzles/Prefab/Save Current Selection")]
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

    public struct Element
    {
        public string line, column, element;
/*
        public Element(string line*//*, string column, string element*//*)
        {
            this.line = line;*//*
            this.column = column;
            this.element = element;*//*
        }*/

    }

    [MenuItem("Rational/Excel")]
    public static void LoadCSV()
    {
        var csvText = Resources.Load<TextAsset>("Puzzle_Rational/SC_#1_Outcome").text;

        string[] lineSeparators = new string[] { "\n", "\r", "\n\r", "\r\n" };
        char[] cellSeparator = new char[] { ',' };

        var lines = csvText.Split(lineSeparators, System.StringSplitOptions.RemoveEmptyEntries);
        //List<Element> completeExcelFile = new List<Element>();

        List<string[]> a = new List<string[]>();

        string[][] sheet = new string[lines.Length][];


        foreach (var i in lines)
        {
            a.Add(i.Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries));//Nombre de character dans tout la ligne
            //Debug.Log(i);
        }

        foreach (var item in a)
        {
            //Debug.Log(item[]); 
        }

        for (int i = 0; i < a.Count; i++)
        {
            //sheet[i][0] = a;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            //sheet[i] = lines[i].Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries);//Nombre de character dans tout la ligne

            //Debug.Log(lines[i] + ", number of character: "+ lines[i].Length); /*Contenu de la ligne + le nombre de character*/

            //var cell = lines[i].Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries);

            //Debug.Log(lines[i]); //Contenu de la ligne
            //Debug.Log(lines[i].Length); // Le nombre de character

            //Debug.Log(lines[i].Split(cellSeparator, System.StringSplitOptions.RemoveEmptyEntries));

            //Objectif séparer le contenue de la ligne
            
            
            
            //Debug.Log(lines[i] + ", number of character: " + lines[i].Length);
        }


        /*  for (var i = 0; i < sheet.Length; i++)
          {
              Debug.Log(sheet[i][]); 
              *//*for (var j = 0; j < sheet[i].Length; j++)
              {
                  *//*
                  Debug.Log(sheet[0][j]);

                  //completeExcelFile.Add(new Element(sheet[0][j], sheet[i][0], sheet[i][j]));
              }*//*
          }*/

        //Debug.Log(completeExcelFile.Count);

        //OrderController.Instance.outcomes = completeExcelFile;

    }

    static void DebugEntry(string[] entry)
    {
        for (int i = 1; i < 16; i++)
        {
            if (entry[i].Contains("Null"))
            {
                Debug.Log(entry[0] + ", " + entry[i]);
            }
        }
        
    }
#endif
}

public class LoadCSV : ScriptableObject
{
    public TextAsset sc1, sc2, sc3;

    public void Init()
    {
        sc1 = Resources.Load<TextAsset>("Puzzle_Rational/SC_#1");
        sc2 = Resources.Load<TextAsset>("Puzzle_Rational/SC_#2");
        sc3 = Resources.Load<TextAsset>("Puzzle_Rational/SC_#3");
    }
}




