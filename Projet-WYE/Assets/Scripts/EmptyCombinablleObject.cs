using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine.Events;

#if UNITY_EDITOR
public class EmptyCombinablleObject : CombinableObject_Data
{
    public void GenerateComponentsForCombinableObjects()
    {
        this.name = this.gameObject.name;

        GameObject current = this.gameObject;

        var co = current.GetComponent<CombinableObject>();

        //System.Array.Clear(co.useWith, 0, co.useWith.Length);

        if (co == null)
        {
            co = current.AddComponent<CombinableObject>();
        }
        co.GetComponent();

        if (!co.sphereCollider)
        {
            co.sphereCollider = current.AddComponent<SphereCollider>();
        }
        if (!co.outline)
        {
            co.outline = current.AddComponent<Outline>();
        }
        if (!co.dissolveEffect)
        {
            co.dissolveEffect = current.AddComponent<DissolveEffect>();
            co.dissolveEffect.Init(false);
        }

        if (state == StateMobility.Dynamic)
        {

            XRGrabInteractableWithAutoSetup xrInteractable = current.GetComponent<XRGrabInteractableWithAutoSetup>();

            if (xrInteractable == null)
            {
                xrInteractable = current.AddComponent<XRGrabInteractableWithAutoSetup>();
            }

            var mustImplementToogleOutline = true;

            for (int i = 0; i < xrInteractable.hoverEntered.GetPersistentEventCount(); i++)
            {
                if (xrInteractable.hoverEntered.GetPersistentMethodName(i) == "ToggleOutline")
                {
                    mustImplementToogleOutline = false;
                }
            }

            if (mustImplementToogleOutline)
            {
                UnityAction<bool> action1 = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action1, true);

                UnityAction<bool> action2 = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action2, false);

            }
        }
        else if (state == StateMobility.Static)
        {
            XRSimpleInteractableWithAutoSetup xrInteractable = current.GetComponent<XRSimpleInteractableWithAutoSetup>();
            if (xrInteractable == null)
            {
                xrInteractable = current.AddComponent<XRSimpleInteractableWithAutoSetup>();
            }
            var mustImplementToogleOutline = true;
            for (int i = 0; i < xrInteractable.hoverEntered.GetPersistentEventCount(); i++)
            {
                if (xrInteractable.hoverEntered.GetPersistentMethodName(i) == "ToggleOutline")
                {
                    mustImplementToogleOutline = false;
                }
            }
            if (mustImplementToogleOutline)
            {
                UnityAction<bool> action1 = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action1, true);

                UnityAction<bool> action2 = new UnityAction<bool>(co.ToggleOutline);
                UnityEventTools.AddBoolPersistentListener(xrInteractable.hoverEntered, action2, false);
            }
        }
    }
}

[CanEditMultipleObjects]
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