using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PhysicsButton)), CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    SerializedProperty 
        sp_modeProp,

        sp_treshold,
        sp_deadzone,
        sp_childObject,

        sp_unitToSendProp,
        sp_isPressedProp,
        sp_clickerProp,
        sp_unlockColorProp,
        sp_isActivateProp,

        sp_onPressed,
        sp_onReleased;

    public void OnEnable()
    {
        sp_modeProp = serializedObject.FindProperty("currentMode");

        sp_treshold = serializedObject.FindProperty("treshold");
        sp_deadzone = serializedObject.FindProperty("deadZone");
        sp_childObject = serializedObject.FindProperty("childObject");

        sp_unitToSendProp = serializedObject.FindProperty("unitToSend");
        sp_clickerProp = serializedObject.FindProperty("clicker");
        sp_unlockColorProp = serializedObject.FindProperty("unlockColor");
        sp_isActivateProp = serializedObject.FindProperty("isActivate");

        sp_onPressed = serializedObject.FindProperty("onPressed");
        sp_onReleased = serializedObject.FindProperty("onReleased");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(sp_modeProp);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(sp_treshold);
        EditorGUILayout.PropertyField(sp_deadzone);
        EditorGUILayout.PropertyField(sp_childObject);

        PhysicsButton.Mode displayMode = (PhysicsButton.Mode)sp_modeProp.enumValueIndex;

        switch (displayMode)
        {
            case PhysicsButton.Mode.Unit:

                EditorGUILayout.PropertyField(sp_unitToSendProp);

                EditorGUILayout.PropertyField(sp_clickerProp);
                EditorGUILayout.PropertyField(sp_unlockColorProp);
                EditorGUILayout.PropertyField(sp_isActivateProp);

                EditorGUILayout.Space(10);
                EditorGUILayout.PropertyField(sp_onPressed);
                EditorGUILayout.PropertyField(sp_onReleased);
                break;

            case PhysicsButton.Mode.Physic:
                EditorGUILayout.Space(10);
                EditorGUILayout.PropertyField(sp_onPressed);
                EditorGUILayout.PropertyField(sp_onReleased);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
