using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PhysicsButton)), CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    SerializedProperty 
        sp_modeProp,

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

        sp_childObject = serializedObject.FindProperty("push");

        sp_unitToSendProp = serializedObject.FindProperty("unitToSend");
        sp_clickerProp = serializedObject.FindProperty("clicker");
        sp_unlockColorProp = serializedObject.FindProperty("unlockColor");
        sp_isActivateProp = serializedObject.FindProperty("isActivate");

        sp_onPressed = serializedObject.FindProperty("onPressed");
        sp_onReleased = serializedObject.FindProperty("onReleased");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


       /* serializedObject.Update();

        EditorGUILayout.PropertyField(sp_modeProp);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(sp_childObject);

        PhysicsButton.Mode displayMode = (PhysicsButton.Mode)sp_modeProp.enumValueIndex;

        switch (displayMode)
        {
            case PhysicsButton.Mode.Unit:

                EditorGUILayout.PropertyField(sp_unitToSendProp);
                EditorGUILayout.PropertyField(sp_unlockColorProp);
                EditorGUILayout.PropertyField(sp_isActivateProp);
                break;

            case PhysicsButton.Mode.Physic:
                break;
        }
        serializedObject.ApplyModifiedProperties();*/
    }
}
