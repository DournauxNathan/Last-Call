using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestionFormat))]
public class QuestionFormatEditor : Editor
{
    private SerializedProperty sp_listQuestion;
    private SerializedProperty sp_voiceLine;
    private SerializedProperty sp_listIdObject;

    public void OnEnable()
    {
        sp_listQuestion = serializedObject.FindProperty("listQuestion");
        sp_voiceLine = serializedObject.FindProperty("voiceLine");
        sp_listIdObject = serializedObject.FindProperty("listIdObject");
    }

    public override void OnInspectorGUI()
    {
        if(sp_listQuestion == null)
        {
            (target as QuestionFormat).listQuestion = new string[0];
        }

        if (sp_voiceLine == null)
        {
            (target as QuestionFormat).voiceLine = new AudioClip[0];
        }

        for (int i = 0; i < sp_listQuestion.arraySize; i++)
        {
            if(sp_voiceLine.GetArrayElementAtIndex(i) == null)
            {
                sp_voiceLine.InsertArrayElementAtIndex(i);
            }
            DisplayArrayElement(i);
        }

        if(GUILayout.Button("Create"))
        {
            CreateElement();
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayArrayElement(int index)
    {
        var _currentVoice = sp_voiceLine.GetArrayElementAtIndex(index);
        var _currentQuestion = sp_listQuestion.GetArrayElementAtIndex(index);

        EditorGUILayout.LabelField(_currentQuestion.stringValue, EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentQuestion, new GUIContent(""));
        EditorGUILayout.PropertyField(_currentVoice, new GUIContent(""));   
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("IdObject");

        if(GUILayout.Button("+"))
        {
            CreateIDObject(index);
        }

        EditorGUILayout.EndHorizontal();
        DisplayIdObject(index);

        if (GUILayout.Button("Delete"))
        {
            DeleteElement(index);
        }
    }

    private void DeleteElement(int index)
    {
        sp_listQuestion.DeleteArrayElementAtIndex(index);
        sp_voiceLine.DeleteArrayElementAtIndex(index);

        for (int i = 0; i < sp_listIdObject.arraySize; i++)
        {
            var _currentID = sp_listIdObject.GetArrayElementAtIndex(i);
            if (_currentID.vector2IntValue.y == index)
            {
                sp_listIdObject.DeleteArrayElementAtIndex(i);
                
            }
        }
    }
    private void DisplayIdObject(int index)
    {
        for (int i = 0; i < sp_listIdObject.arraySize; i++)
        {
            var _currentID = sp_listIdObject.GetArrayElementAtIndex(i);

            if(_currentID.vector2IntValue.y == index)
            {
                EditorGUILayout.BeginHorizontal();

                var _temp = EditorGUILayout.IntField(_currentID.vector2IntValue.x);
                _currentID.vector2IntValue = new Vector2Int(_temp, index);

                if (GUILayout.Button("x"))
                {
                    sp_listIdObject.DeleteArrayElementAtIndex(i);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void CreateElement()
    {
        var _newElementIndex = sp_listQuestion.arraySize;
        sp_listQuestion.InsertArrayElementAtIndex(_newElementIndex);
        sp_voiceLine.InsertArrayElementAtIndex(_newElementIndex);

        CreateIDObject(_newElementIndex);
    }
    private void CreateIDObject(int yIndex)
    {
        var _newElementIndex = sp_listIdObject.arraySize;
        sp_listIdObject.InsertArrayElementAtIndex(_newElementIndex);
        sp_listIdObject.GetArrayElementAtIndex(_newElementIndex).vector2IntValue = new Vector2Int(0, yIndex);
    }
}
