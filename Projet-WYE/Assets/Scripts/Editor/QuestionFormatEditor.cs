using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestionFormat))]
public class QuestionFormatEditor : Editor
{
    private SerializedProperty sp_listQuestion;
    private SerializedProperty sp_voiceLineQuestion;
    private SerializedProperty sp_voiceLineAnswer;
    private SerializedProperty sp_listAnswers;
    private SerializedProperty sp_listIdObject;
    private SerializedProperty sp_unit;

    public void OnEnable()
    {
        sp_listQuestion = serializedObject.FindProperty("listQuestion");
        sp_listAnswers = serializedObject.FindProperty("listAnswers");
        sp_voiceLineQuestion = serializedObject.FindProperty("voiceLineQuestion");
        sp_voiceLineAnswer = serializedObject.FindProperty("voiceLineAnswer");
        sp_listIdObject = serializedObject.FindProperty("listIdObject");
        sp_unit = serializedObject.FindProperty("units");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (sp_listQuestion == null)
        {
            (target as QuestionFormat).listQuestion = new string[0];
        }

        if (sp_listAnswers == null)
        {
            (target as QuestionFormat).listAnswers = new string[0];
        }

        if (sp_voiceLineQuestion == null)
        {
            (target as QuestionFormat).voiceLineQuestion = new AudioClip[0];
        }

        if (sp_voiceLineAnswer == null)
        {
            (target as QuestionFormat).voiceLineAnswer = new AudioClip[0];
        }

        if (sp_unit == null)
        {
            (target as QuestionFormat).units = new List<Unit>();
        }

        for (int i = 0; i < sp_listQuestion.arraySize; i++)
        {
            if (sp_voiceLineQuestion.GetArrayElementAtIndex(i) == null)
            {
                sp_voiceLineQuestion.InsertArrayElementAtIndex(i);
            }
            if (sp_voiceLineAnswer.GetArrayElementAtIndex(i) == null)
            {
                sp_voiceLineAnswer.InsertArrayElementAtIndex(i);
            }

            DisplayArrayElement(i);
        }

        if (GUILayout.Button("Create"))
        {
            CreateElement();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DisplayArrayElement(int index)
    {
        var _currentAnswer = sp_listAnswers.GetArrayElementAtIndex(index);
        var _currentQuestion = sp_listQuestion.GetArrayElementAtIndex(index);

        var _currentVoiceQuestion = sp_voiceLineQuestion.GetArrayElementAtIndex(index);
        var _currentVoiceAnswer = sp_voiceLineQuestion.GetArrayElementAtIndex(index);

        var _currentUnit = sp_unit.GetArrayElementAtIndex(index);

        EditorGUILayout.LabelField(_currentQuestion.stringValue, EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentQuestion, new GUIContent(""));
        EditorGUILayout.PropertyField(_currentVoiceQuestion, new GUIContent(""));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField(_currentAnswer.stringValue, EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_currentAnswer, new GUIContent(""));
        EditorGUILayout.PropertyField(_currentVoiceAnswer, new GUIContent(""));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ID", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Unit To Send", EditorStyles.boldLabel);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        DisplayIdObject(index);
        EditorGUILayout.PropertyField(_currentUnit, new GUIContent(""));

        if (GUILayout.Button("Delete"))
        {
            DeleteElement(index);
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DeleteElement(int index)
    {
        sp_listQuestion.DeleteArrayElementAtIndex(index);
        sp_voiceLineQuestion.DeleteArrayElementAtIndex(index);

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

            if (_currentID.vector2IntValue.y == index)
            {
                EditorGUILayout.BeginHorizontal();

                var _temp = EditorGUILayout.IntField(_currentID.vector2IntValue.x);
                _currentID.vector2IntValue = new Vector2Int(_temp, index);

                /*if (GUILayout.Button("x"))
                {
                    sp_listIdObject.DeleteArrayElementAtIndex(i);
                }*/

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void CreateElement()
    {
        var _newElementIndex = sp_listQuestion.arraySize;
        sp_listQuestion.InsertArrayElementAtIndex(_newElementIndex);
        sp_voiceLineQuestion.InsertArrayElementAtIndex(_newElementIndex);
        sp_unit.InsertArrayElementAtIndex(_newElementIndex);

        CreateIDObject(_newElementIndex);
    }

    private void CreateIDObject(int yIndex)
    {
        var _newElementIndex = sp_listIdObject.arraySize;
        sp_listIdObject.InsertArrayElementAtIndex(_newElementIndex);
        sp_listIdObject.GetArrayElementAtIndex(_newElementIndex).vector2IntValue = new Vector2Int(0, yIndex);
    }
}
