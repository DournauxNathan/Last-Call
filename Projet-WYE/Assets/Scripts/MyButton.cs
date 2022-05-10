using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MyButton : Button
{
    [Header("Debug")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private TMP_Text _text;


    protected override void Start()
    {
        base.Start();
        if (_text != null)
        {
            _text.text = "";

        }

    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        if (name == "Load" && inputHandler != null)
        {
            _text.text = "";
            _text.text = inputHandler.LoadSaveData();    
        }

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MyButton))]
public class MyButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MyButton targetMyButton = (MyButton)target;

        //targetMyButton.acceptsPointerInput = EditorGUILayout.Toggle("Accepts pointer input", targetMyButton.acceptsPointerInput);

        base.OnInspectorGUI();

    }

}



#endif
