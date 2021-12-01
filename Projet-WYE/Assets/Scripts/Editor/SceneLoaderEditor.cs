using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    SerializedProperty sp_sceneName;

    public void OnEnable()
    {
        sp_sceneName = serializedObject.FindProperty("nameScene");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SceneLoader sceneLoader = (SceneLoader)target;
        
        EditorGUILayout.Space(10);
        EditorGUILayout.PropertyField(sp_sceneName, new GUIContent(""));

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Change Scene"))
        {
            sceneLoader.LoadScene();
        }
        EditorGUILayout.EndHorizontal();
    }
}
