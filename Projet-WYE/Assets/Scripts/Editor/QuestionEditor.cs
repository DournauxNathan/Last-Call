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
        sp_question = serializedObject.FindProperty("questions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.ApplyModifiedProperties();

        for (int i = 0; i < sp_question.arraySize; i++)
        {
            DisplayArrayElement(i);
        }

        if (GUILayout.Button("Add Question"))
        {
            CreateElement();
        }
    }

    public void DisplayArrayElement(int index)
    {
        var _currentQuestion = sp_question.GetArrayElementAtIndex(index);
        var _currentDisplayAt = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("displayAt");
        var _currentText = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("question");
        var _currentAnswer = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("answer");
        var _currentVoice = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("voices");
        var _materials = sp_question.GetArrayElementAtIndex(index).FindPropertyRelative("linkObjects");

        EditorGUILayout.Space(10);

        //EditorGUILayout.LabelField(new GUIContent(""), GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField(new GUIContent("QMP_" + (index + 1)), EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(_currentText, new GUIContent("Question"));
        EditorGUILayout.PropertyField(_currentAnswer, new GUIContent("Answer"));
        EditorGUILayout.PropertyField(_currentDisplayAt, new GUIContent("DisplayAt"));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentVoice, new GUIContent(""));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(_materials, new GUIContent("Object"));

        EditorGUILayout.BeginHorizontal();
        
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Delete", GUILayout.Width(350), GUILayout.Height(20)))
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
