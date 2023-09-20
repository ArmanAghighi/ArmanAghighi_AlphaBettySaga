using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridCreator))] 
public class LevelDesigner : Editor
{
    private const float boxSize = 25f;
    private const float boxSpacing = 10f;
    private bool _isSelected = false;
    private string GetPlayerPrefsKey(int row, int col, GridCreator gridCreator)
    {
        return $"BoxSelection_{gridCreator.GetInstanceID()}_{row}_{col}";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GridCreator gridCreator = (GridCreator)target;

        // Check if the boxSelected array is null and initialize it if needed
        if (gridCreator.boxSelected == null)
        {
            gridCreator.InitializeGrid(gridCreator.gridSize);
        }

        GUILayout.Space(20);

        GUILayout.Label("Grid Preview", EditorStyles.boldLabel);
        if (gridCreator.gridSize >= 10)
            gridCreator.gridSize = 10;
        if(gridCreator.gridSize <= 4)
            gridCreator.gridSize = 4;

        for (int row = 0; row < gridCreator.gridSize; row++)
        {
            GUILayout.BeginHorizontal();

            for (int col = 0; col < gridCreator.gridSize; col++)
            {
                bool boxSelected = PlayerPrefs.GetInt(GetPlayerPrefsKey(row, col, gridCreator)) == 1;
                Color boxColor = boxSelected ? Color.green : Color.gray;

                Rect boxRect = GUILayoutUtility.GetRect(boxSize, boxSize, GUILayout.ExpandWidth(false));
                EditorGUI.DrawRect(boxRect, boxColor);

                if (Event.current.type == EventType.MouseDown && boxRect.Contains(Event.current.mousePosition))
                {
                    boxSelected = !boxSelected;
                    PlayerPrefs.SetInt(GetPlayerPrefsKey(row, col, gridCreator), boxSelected ? 1 : 0);
                    PlayerPrefs.Save();

                    gridCreator.ToggleBoxSelection(row, col);
                    GUI.changed = true;
                }

                GUILayout.Space(boxSpacing);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(boxSpacing);
        }
    }
}