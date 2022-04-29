using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(MySlider))]
public class MySliderEditor : SliderEditor
{
    private SerializedProperty sp_onValueChangedWithOldValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        sp_onValueChangedWithOldValue = serializedObject.FindProperty("onValueChangedWithOldValue");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.PropertyField(sp_onValueChangedWithOldValue);
        serializedObject.ApplyModifiedProperties();
    }
}
