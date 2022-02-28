using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EmptyCombinablleObject : CombinableObject_Data
{
    public void GenerateComponentsForCombinableObjects()
    {
        GameObject current = this.gameObject;

        current.AddComponent<MeshFilter>();
        current.AddComponent<MeshRenderer>();
        current.AddComponent<MeshCollider>();

        if (state == StateMobility.Dynamic)
        {
            current.AddComponent<XRGrabInteractableWithAutoSetup>();
        }
        else if (state == StateMobility.Static)
        {
            current.AddComponent<XRSimpleInteractableWithAutoSetup>();
        }

        current.AddComponent<SphereCollider>();
        current.AddComponent<Outline>();

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EmptyCombinablleObject))]
public class GenerateComponentsEditor : Editor
{
    EmptyCombinablleObject script;

    public override void OnInspectorGUI()
    { 
        script = target as EmptyCombinablleObject;

        DrawDefaultInspector();

        if (GUILayout.Button("Add Components"))
        {
            script.GenerateComponentsForCombinableObjects();
        }

        serializedObject.ApplyModifiedProperties();
    }

}
#endif