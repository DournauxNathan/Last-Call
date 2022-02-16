
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Write here the type of the class or struct affected by the property Drawer
//[CustomPropertyDrawer(typeof(TYPE))]

public abstract class BasePropertyDrawer : PropertyDrawer
{
    //Choose Number of line used By the propery drawer.
    internal int numberOfLine = 1;
    internal List<float> currentXList;

    //Property parameters
    private Rect position;
    private SerializedProperty property;
    private float usableSpace;
    internal object target;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return (EditorGUIUtility.singleLineHeight + 1) * numberOfLine;
    }



    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        
        target = fieldInfo.GetValue(property.serializedObject.targetObject);

        AtStartOfGUI(property);

        //Set Parameters
        this.position = position;
        this.property = property;

        //Construct Drawer rect

        int currentIndentation = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        float indentationPixel = currentIndentation * 16;
        SetCurrentXList(position.x + indentationPixel);

        usableSpace = position.width - indentationPixel;

        OnGUIEffect(position, property);

        EditorGUI.indentLevel = currentIndentation;

        property.serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Method called at the start of GUI before anything else. Use it to change the number of line of your PropertyDrawer
    /// </summary>
    internal virtual void AtStartOfGUI(SerializedProperty property)
    {

    }

    /// <summary>
    /// Effect That apply in OnGUI Method
    /// </summary>
    internal virtual void OnGUIEffect(Rect position, SerializedProperty property)
    {
        Rect _labelRect = MakeRectForDrawer(1f, 0f);
        EditorGUI.LabelField(_labelRect, "This is a test label, please modify the virtual OnGUIEffect Function");
    }

    private void SetCurrentXList(float currentXbase)
    {
        currentXList = new List<float>();

        for (int i = 0; i < numberOfLine; i++)
        {
            currentXList.Add(currentXbase);
        }

    }

    /// <summary>
    /// Return a Rect on the entred collumn and that take the entred ySize.
    /// </summary>
    /// <param name="collumn"></param>
    /// <param name="usedSpace"></param>
    /// <param name="ySize"></param>
    /// <param name="spaceAfterElement"></param>
    /// <returns></returns>
    internal Rect MakeRectForDrawer(int collumn, float usedSpace, int ySize, float spaceAfterElement)
    {
        if (collumn > numberOfLine)
        {
            Debug.LogError("You can't draw elements in a line above the number of line of the elements");
            return new Rect(0, 0, 0, 0);
        }

        Rect _retRect = new Rect(currentXList[collumn], position.y + collumn * EditorGUIUtility.singleLineHeight, usedSpace * usableSpace, (EditorGUIUtility.singleLineHeight + 1) * ySize);

        currentXList[collumn] += _retRect.width + usableSpace * spaceAfterElement;


        return _retRect;
    }

    /// <summary>
    /// Return a Rect that take the whole place allowed.
    /// </summary>
    /// <param name="usedSpace"></param>
    /// <param name="spaceAfterElement"></param>
    /// <returns></returns>
    internal Rect MakeRectForDrawer(float usedSpace, float spaceAfterElement)
    {
        Rect _retRect = new Rect(currentXList[0], position.y, usedSpace * usableSpace, position.height);
        currentXList[0] += _retRect.width + usableSpace * spaceAfterElement;
        return _retRect;
    }
}
