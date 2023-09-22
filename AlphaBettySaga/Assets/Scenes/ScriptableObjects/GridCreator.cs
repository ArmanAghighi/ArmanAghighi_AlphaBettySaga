using UnityEngine;

[CreateAssetMenu(fileName = "GridCreator", menuName = "Create GridCreator")]
public class GridCreator : ScriptableObject
{
    public bool[,] boxSelected;
    public int gridSize = 10;

    public string GetPlayerPrefsKey(int row, int col)
    {
        return $"BoxSelection_{this.GetInstanceID()}_{row}_{col}";
    }

    public void InitializeGrid(int size)
    {
        ClearPlayerPrefs();
        gridSize = size;
        boxSelected = new bool[gridSize, gridSize];

        // Load box selection state from PlayerPrefs
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                var key = GetPlayerPrefsKey(i, j);
                boxSelected[i, j] = PlayerPrefs.GetInt(key) == 1;
            }
        }
    }

    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    public bool ToggleBoxSelection(int row, int col)
    {
        // Toggle selection 
        boxSelected[row, col] = !boxSelected[row, col];

        // Save to PlayerPrefs
        SaveBoxSelection();

        return boxSelected[row, col];
    }

    private void SaveBoxSelection()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                var key = GetPlayerPrefsKey(i, j);
                PlayerPrefs.SetInt(
                    key,
                    boxSelected[i, j] ? 1 : 0
                );
            }
        }

        PlayerPrefs.Save();
    }

    public void GridData()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Debug.Log(boxSelected[i, j]);
            }
        }
    }
}