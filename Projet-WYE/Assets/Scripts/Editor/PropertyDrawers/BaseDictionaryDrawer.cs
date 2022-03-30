using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(BaseDictionary),true)]
public class BaseDictionaryDrawer : BasePropertyDrawer
{
    private BaseDictionary dictionaryTarget;
    private SerializedProperty
        keysProperty,
        valueProperty;
    private bool foldout;
    private System.Action atEndOfFrame;


    internal override void AtStartOfGUI(SerializedProperty property)
    {
        dictionaryTarget = target as BaseDictionary;

        numberOfLine = foldout?dictionaryTarget.Size+2:1; //One for each element, one for the title and one for the [+]
    }

    internal override void OnGUIEffect(Rect position, SerializedProperty property)
    {
        //set rects
        var _rectLabel = MakeRectForDrawer(0, 1, 1, 0);
        var _rectButton = MakeRectForDrawer(numberOfLine - 1, .3f, 1, 0);
        
        //Get keys and values properties
        keysProperty = property.FindPropertyRelative("keys");
        valueProperty = property.FindPropertyRelative("values");

        //Draw the name of the element
        var _tempFoldout = EditorGUI.Foldout(_rectLabel,foldout, property.name,true);

        if(foldout)
        {
            //If its ok draw all elements
            if(dictionaryTarget.IsValid)
            {
                for (int i = 0; i < keysProperty.arraySize; i++)
                {
                    DrawElementLine(i);
                }
            }

            //Draw the add button 
            var _but = GUI.Button(_rectButton, "Add");

            //set Add to the end delegate if the button is pressed
            atEndOfFrame = _but ? dictionaryTarget.Add : atEndOfFrame;
        }

        //Call the end of frame delegate if it exist then delete it
        if (atEndOfFrame!=null)
        {
            atEndOfFrame.Invoke();
            atEndOfFrame = null;
        }

        foldout = _tempFoldout;
    }

    private void DrawElementLine(int elementNumber)
    {
        //Set the rects
        var _rectLabel = MakeRectForDrawer(elementNumber+1, 0.2f,1, 0f);
        var _rectKey = MakeRectForDrawer(elementNumber+1, 0.35f,1, 0f);
        var _rectValue = MakeRectForDrawer(elementNumber+1, 0.35f,1, 0f);
        var _rectCross = MakeRectForDrawer(elementNumber+1, 0.1f,1, 0f);


        //Display the element name
        EditorGUI.LabelField(_rectLabel, dictionaryTarget.ElementName(elementNumber));

        //Display the values
        EditorGUI.PropertyField(_rectKey, keysProperty.GetArrayElementAtIndex(elementNumber), new GUIContent(""));
        EditorGUI.PropertyField(_rectValue, valueProperty.GetArrayElementAtIndex(elementNumber), new GUIContent(""));

        //Display the supressor of button
        var _but = GUI.Button(_rectCross, ("x"), EditorStyles.miniButton);

        //Set end of frame to supress entry if the button is pressed
        atEndOfFrame = _but ? ()=> dictionaryTarget.Remove(elementNumber) : atEndOfFrame;
    }
}
