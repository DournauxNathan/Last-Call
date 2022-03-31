using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(Question))]
public class QuestionEditor : Editor
{
    SerializedProperty 
        sp_question, 
        sp_reference;

    int i = 0;


    public void OnEnable()
    {
        sp_question = serializedObject.FindProperty("question");
        sp_reference = sp_question.FindPropertyRelative("reference");

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
            i++;
        }

        EditorGUILayout.Space(5);

        serializedObject.ApplyModifiedProperties();
    }

    public void DisplayArrayElement(int index)
    {
        var _currentQuestion = sp_question.GetArrayElementAtIndex(index);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentQuestion, new GUIContent("QMP_"+i));
        EditorGUILayout.EndHorizontal();


        if (GUILayout.Button("Delete"))
        {
            DeleteElement(index);
        }
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
