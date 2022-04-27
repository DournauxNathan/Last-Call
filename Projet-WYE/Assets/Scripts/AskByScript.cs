using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AskByScript : MonoBehaviour
{
    public int[] atIndex = new int[] { 0,0,0 };
    public bool askProtocolQuestion;
    public bool askDescriptionQuestion;
    public bool giveOrder;
    
    void LateUpdate()
    {
        if (askProtocolQuestion && atIndex[0] > -1)
        {
            DoAtIndex(atIndex[0], WordManager.Instance.transform);
            askProtocolQuestion = false;
        } 
        else if (askProtocolQuestion)
        {
            DoForAll(WordManager.Instance.transform);
        }

        if (askDescriptionQuestion && atIndex[1] > -1)
        {
            DoAtIndex(atIndex[1], WordManager.Instance.transform);
            askDescriptionQuestion = false;
        }
        else if(askDescriptionQuestion)
        {
            DoForAll(WordManager.Instance.transform);
            //askDescriptionQuestion = false;
        }

        if (giveOrder && atIndex[2] > -1)
        {
            DoAtIndex(atIndex[2], WordManager.Instance.transform);
            giveOrder = false;
        }
        else if (giveOrder)
        {
            DoForAll(WordManager.Instance.transform);
            //giveOrder = false;
        }
    }
    public void DoAtIndex(int index,Transform _transform)
    {
        if (_transform.GetChild(index).TryGetComponent<WordData>(out WordData _data))
        {
            _transform.GetChild(index).GetComponent<WordData>().simulateInput = true;
        }
        else if (_transform.GetChild(index).TryGetComponent<Reveal>(out Reveal _reveal))
        { 
            _transform.GetChild(index).GetComponent<Reveal>().simulateInput = true;
        }

    }
    public void DoForAll(Transform _transform)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            if (_transform.GetChild(i).TryGetComponent<WordData>(out WordData _data))
            {
                _transform.GetChild(i).GetComponent<WordData>().simulateInput = true;
            }
            else if (_transform.GetChild(i).TryGetComponent<Reveal>(out Reveal _reveal))
            {
                _transform.GetChild(i).GetComponent<Reveal>().simulateInput = true;
            }
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(AskByScript))]
public class AskByScriptEditor : Editor
{
    SerializedProperty sp_boola, sp_boolb, sp_boolc, sp_atIndex;
    AskByScript script;
    
    private void OnEnable()
    {
        sp_boola = serializedObject.FindProperty("askProtocolQuestion");
        sp_boolb = serializedObject.FindProperty("askDescriptionQuestion");
        sp_boolc = serializedObject.FindProperty("giveOrder");
        sp_atIndex = serializedObject.FindProperty("atIndex");
    }
    public override void OnInspectorGUI()
    {
        script = target as AskByScript;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sp_boola, new GUIContent("Ask Protocol Question"));
        EditorGUILayout.PropertyField(sp_atIndex.GetArrayElementAtIndex(0), new GUIContent(""));
        
        if (GUILayout.Button("All"))
        {
            script.DoForAll(WordManager.Instance.transform);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sp_boolb, new GUIContent("Ask Description Question"));
        EditorGUILayout.PropertyField(sp_atIndex.GetArrayElementAtIndex(1), new GUIContent(""));
        if (GUILayout.Button("All"))
        {
            script.DoForAll(WordManager.Instance.transform);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sp_boolc, new GUIContent("Give Order"));
        EditorGUILayout.PropertyField(sp_atIndex.GetArrayElementAtIndex(2), new GUIContent(""));
        
        if (GUILayout.Button("All"))
        {
            script.DoForAll(WordManager.Instance.transform);
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }

}
#endif