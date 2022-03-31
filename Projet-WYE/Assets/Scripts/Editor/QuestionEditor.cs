using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Question), true)]
public class QuestionEditor : Editor
{
    SerializedProperty
        sp_question; 
        
    public void OnEnable()
    {
        sp_question = serializedObject.FindProperty("question");
    }

    public override void OnInspectorGUI()
    {
        for (int i = 0; i < sp_question.arraySize; i++)
        {
            DisplayArrayElement(i);
        }

        if (GUILayout.Button("Add Question"))
        {
            CreateElement();
        }

        serializedObject.ApplyModifiedProperties();
    }

    public void DisplayArrayElement(int index)
    {
        var _currentQuestion = sp_question.GetArrayElementAtIndex(index);
        var _currentText = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("text");
        var _currentVoice = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("voices");
        var _materials = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("linkObjects");

        EditorGUILayout.Space(10);

        //EditorGUILayout.LabelField(new GUIContent(""), GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField(new GUIContent("QMP_" + index), EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentText, new GUIContent(""));
        EditorGUILayout.PropertyField(_currentVoice, new GUIContent(""));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(_materials, new GUIContent("Object"));

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Delete",  GUILayout.Width(350), GUILayout.Height(20)))
        {
            DeleteElement(index);
        }   
        EditorGUILayout.EndHorizontal();
    }   

    public void CreateElement()
    {
        var _newElementIndex = sp_question.arraySize;
        sp_question.InsertArrayElementAtIndex(_newElementIndex);
    }

    private void DeleteElement(int index)
    {
        sp_question.DeleteArrayElementAtIndex(index);
    }
}
#endif
