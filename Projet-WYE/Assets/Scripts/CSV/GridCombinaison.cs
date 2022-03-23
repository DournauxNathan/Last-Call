using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[System.Serializable]
public class Level
{
#if UNITY_EDITOR
    [HideInInspector] public bool showBoard;
#endif
    public int columns = 9;
    public int rows = 9;

    public string[,] board = new string[9, 9];
}

public class GridCombinaison : MonoBehaviour
{
    public Level[] allOutcomes = new Level[1];

    public TextAsset csvFile;
    public string[,] csvData; // This is your multidimensional array that stores the parsed CSV data.

    public string[] textRow;

    public void Start()
    {
        csvData = CSVReader.SplitCsvGrid(csvFile.text);
        Debug.Log(csvData.GetUpperBound(0) + 1);    

        textRow = new string[csvData.GetUpperBound(0) + 1];  // Instantiate the textRow array with the number of rows in csvData array.

        for (int i = 0; i < textRow.Length; i++)
        {
            textRow[i] = csvData[i, 0];
        }
    }
}
#if UNITY_EDITOR
//[CustomEditor(typeof(GridCombinaison)), CanEditMultipleObjects]
public class LevelEditor : Editor
{
    public bool showLevels = true;

    public override void OnInspectorGUI()
    {
        GridCombinaison grid = (GridCombinaison)target;
        
        EditorGUILayout.Space();

        DrawDefaultInspector();

        showLevels = EditorGUILayout.Foldout(showLevels, "Levels (" + grid.allOutcomes.Length + ")");
        
        if (showLevels)
        {
            EditorGUI.indentLevel++;
            for (ushort i = 0; i < grid.allOutcomes.Length; i++)
            {
                EditorGUI.indentLevel = 0;

                GUIStyle tableStyle = new GUIStyle("box");
                tableStyle.padding = new RectOffset(10, 10, 10, 10);
                tableStyle.margin.left = 32;

                GUIStyle headerColumnStyle = new GUIStyle();
                headerColumnStyle.fixedWidth = 35;

                GUIStyle columnStyle = new GUIStyle();
                columnStyle.fixedWidth = 65;

                GUIStyle rowStyle = new GUIStyle();
                rowStyle.fixedHeight = 25;

                GUIStyle rowHeaderStyle = new GUIStyle();
                rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

                GUIStyle columnHeaderStyle = new GUIStyle();
                columnHeaderStyle.fixedWidth = 30;
                columnHeaderStyle.fixedHeight = 25.5f;

                GUIStyle columnLabelStyle = new GUIStyle();
                columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
                columnLabelStyle.alignment = TextAnchor.MiddleCenter;
                columnLabelStyle.fontStyle = FontStyle.Bold;

                GUIStyle cornerLabelStyle = new GUIStyle();
                cornerLabelStyle.fixedWidth = 42;
                cornerLabelStyle.alignment = TextAnchor.MiddleRight;
                cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
                cornerLabelStyle.fontSize = 14;
                cornerLabelStyle.padding.top = -5;

                GUIStyle rowLabelStyle = new GUIStyle();
                rowLabelStyle.fixedWidth = 25;
                rowLabelStyle.alignment = TextAnchor.MiddleRight;
                rowLabelStyle.fontStyle = FontStyle.Bold;

                GUIStyle enumStyle = new GUIStyle("popup");
                rowStyle.fixedWidth = 65;

                EditorGUILayout.BeginHorizontal(tableStyle);
                for (int x = -1; x < grid.allOutcomes[i].columns; x++)
                {
                    EditorGUILayout.BeginVertical((x == -1) ? headerColumnStyle : columnStyle);
                    for (int y = -1; y < grid.allOutcomes[i].rows; y++)
                    {
                        if (x == -1 && y == -1)
                        {
                            EditorGUILayout.BeginVertical(rowHeaderStyle);
                            EditorGUILayout.LabelField("[X,Y]", cornerLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }
                        else if (x == -1)
                        {
                            EditorGUILayout.BeginVertical(columnHeaderStyle);
                            EditorGUILayout.LabelField(y.ToString(), rowLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }
                        else if (y == -1)
                        {
                            EditorGUILayout.BeginVertical(rowHeaderStyle);
                            EditorGUILayout.LabelField(x.ToString(), columnLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }

                        if (x >= 0 && y >= 0)
                        {
                            EditorGUILayout.BeginHorizontal(rowStyle);
                            grid.allOutcomes[i].board[x, y] = EditorGUILayout.TextField(grid.allOutcomes[i].board[x, y]);
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
#endif