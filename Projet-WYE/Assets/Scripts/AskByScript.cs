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
            DoAtIndex(atIndex[0], UIManager.Instance.checkListTransform, askProtocolQuestion);
        } 
        else if (askProtocolQuestion)
        {
            DoForAll(UIManager.Instance.checkListTransform, askProtocolQuestion);
        }

        if (askDescriptionQuestion && atIndex[1] > -1)
        {
            DoAtIndex(atIndex[1], UIManager.Instance.descriptionTransform, askDescriptionQuestion);
        }
        else if(askDescriptionQuestion)
        {
            DoForAll(UIManager.Instance.descriptionTransform, askDescriptionQuestion);
        }

        if (giveOrder && atIndex[2] > -1)
        {
            DoAtIndex(atIndex[2], UIManager.Instance.orderListTransform, giveOrder);
        }
        else if (giveOrder)
        {
            DoForAll(UIManager.Instance.orderListTransform, giveOrder);
        }
    }
    public void DoAtIndex(int index,Transform _transform, bool _bool)
    {
        _transform.GetChild(index).GetComponent<InstantiableButton>().simulateInput = true;
        _bool = false;

    }
    public void DoForAll(Transform _transform, bool _bool)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            _transform.GetChild(i).GetComponent<InstantiableButton>().simulateInput = true;
            _bool = _transform.GetChild(i).GetComponent<InstantiableButton>().simulateInput;
        }
    }
}

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
            script.DoForAll(UIManager.Instance.checkListTransform, script.askProtocolQuestion);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sp_boolb, new GUIContent("Ask Description Question"));
        EditorGUILayout.PropertyField(sp_atIndex.GetArrayElementAtIndex(1), new GUIContent(""));
        if (GUILayout.Button("All"))
        {
            script.DoForAll(UIManager.Instance.descriptionTransform, script.askDescriptionQuestion);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(sp_boolc, new GUIContent("Give Order"));
        EditorGUILayout.PropertyField(sp_atIndex.GetArrayElementAtIndex(2), new GUIContent(""));
        
        if (GUILayout.Button("All"))
        {
            //script.DoForAll(UIManager.Instance., script.giveOrder);
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
}