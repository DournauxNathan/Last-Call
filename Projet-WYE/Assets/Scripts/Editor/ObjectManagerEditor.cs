using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(ObjectManager)), CanEditMultipleObjects]
public class ObjectManagerEditor : Editor
{
    SerializedProperty
        sp_objectType,
        sp_subList,
        sp_data,
        sp_combi,
        sp_order,
        sp_selectOutline,
        
        sp_outline,
        sp_baseColor,
        sp_selectColor;


    public void OnEnable()
    {
        sp_objectType = serializedObject.FindProperty("objectType");
        sp_data = serializedObject.FindProperty("data");
        sp_combi = serializedObject.FindProperty("combinaisons");
        sp_subList = serializedObject.FindProperty("subList");

        sp_selectOutline = serializedObject.FindProperty("selectOutline");
        sp_order = serializedObject.FindProperty("data.resultOrder");

        sp_outline = serializedObject.FindProperty("outline");
        sp_baseColor = serializedObject.FindProperty("baseColor");
        sp_selectColor = serializedObject.FindProperty("selectColor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sp_objectType);
        EditorGUILayout.Space(10);

         ObjectType enumType = (ObjectType)sp_objectType.enumValueIndex;

        switch (enumType)
        {
            case ObjectType.Useful:
                
                EditorGUILayout.PropertyField(sp_data);
                EditorGUILayout.Space(2);
                EditorGUILayout.PropertyField(sp_combi);
                EditorGUILayout.Space(2);
                EditorGUILayout.PropertyField(sp_subList);

                break;

            case ObjectType.Useless:

                EditorGUILayout.PropertyField(sp_order);
                EditorGUILayout.Space(2);
                EditorGUILayout.PropertyField(sp_subList);
                break;

            case ObjectType.None:

                break;
        }
        EditorGUILayout.Space(5);
        EditorGUILayout.BeginFoldoutHeaderGroup(false, "Outline Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(sp_outline);
        EditorGUILayout.PropertyField(sp_selectOutline);

        serializedObject.ApplyModifiedProperties();
    }
}
