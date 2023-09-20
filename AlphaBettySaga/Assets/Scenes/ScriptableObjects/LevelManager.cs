using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GridCreator gridCreator;
    [SerializeField] GameObject _woodPanel;
    [SerializeField] BoxCollider2D _panel;
    private Vector3 _HoffSet;
    private Vector3 _VoffSet;
    float width;
    float height;
    Vector2 size;
    bool _firstObject = false;
    GameObject _firstGameObject;
    bool _lastObject = false;
    GameObject _lastGameObject;
    void Awake()
    {
        gridCreator.boxSelected = new bool[gridCreator.gridSize, gridCreator.gridSize];

        for (var row = 0; row < gridCreator.gridSize; row++)
        {
            for (var col = 0; col < gridCreator.gridSize; col++)
            {

                var key = gridCreator.GetPlayerPrefsKey(row, col);
                gridCreator.boxSelected[row, col] = PlayerPrefs.GetInt(key) == 1;

            }
        }
        _panel = GameObject.FindGameObjectWithTag("MainPanel").GetComponent<BoxCollider2D>();
        size = _panel.size;
        width = size.x;
        height = size.y;
    }
    private void Start()
    {
        if (gridCreator != null)
        {
            int gridSize = gridCreator.gridSize;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    bool boxSelected = gridCreator.boxSelected[row, col];
                    Color boxColor = boxSelected ? Color.green : Color.gray;
                    if (boxColor == Color.green)
                    {
                        GameObject woodPanel = Instantiate(_woodPanel, gameObject.transform.position , Quaternion.identity);
                        if (!_firstObject)
                        {
                            _firstGameObject = woodPanel;
                            _firstObject = true;
                            Debug.Log(_firstGameObject.name);
                        }
                        if (_lastObject)
                        {
                            _lastGameObject = woodPanel;
                            Debug.Log(_lastGameObject.name);
                        }
                        woodPanel.tag = "WoodPanel";
                        woodPanel.transform.localScale = new Vector3(width/(gridCreator.gridSize * 2), height / (gridCreator.gridSize * 2), 0f) ;
                        _HoffSet = new Vector3(woodPanel.transform.localScale.x + 0.35f, 0f, 0f);
                        _VoffSet = new Vector3(-(woodPanel.transform.localScale.x + 0.35f) * gridCreator.gridSize, -(woodPanel.transform.localScale.y + 0.35f), 0f);
                        gameObject.transform.position += _HoffSet;
                    }
                    else
                    {
                        gameObject.transform.position += _HoffSet;
                    }
                    if (row == gridSize - 1 && !_lastObject)
                    {
                        _lastObject = true;
                    }
                }
                gameObject.transform.position += _VoffSet;
            }
            for (int i = 0; i < gridCreator.gridSize; i++)
            {
                if (GameObject.FindGameObjectWithTag("WoodPanel").transform.parent.name != "_center")
                    GameObject.FindGameObjectWithTag("WoodPanel").transform.SetParent(GameObject.Find("Center").transform);
            }

            Vector3 _center = (_firstGameObject.transform.position + _lastGameObject.transform.position) / 2;
            //GameObject.FindGameObjectWithTag("Center").transform.position = new Vector3( + );
            GameObject.FindGameObjectWithTag("Center").transform.position = _center;
        }
        else
        {
            Debug.LogError("GridCreator reference is not assigned in the Inspector.");
        }
    }
}