using UnityEngine;
using UnityEditor;
public class LevelManager : MonoBehaviour
{
    public GridCreator gridCreator;
    [SerializeField] GameObject _woodPanel;
    [SerializeField] public int _score;
    [SerializeField] public int _move;
    private BoxCollider2D _panelBox;
    private Vector3 _HoffSet;
    private Vector3 _VoffSet;
    private float _offSetVar = 0.35f;
    private float width;
    private float height;
    [SerializeField] private float _sizeMultiplyer = 2;
    Vector2 size;
    GameObject _centerPoint;
    void Awake()
    {
        gridCreator.boxSelected = new bool[gridCreator.gridSize, gridCreator.gridSize];
        // Panel Creator & Center Point Creation Starts
        GameObject _panel = new GameObject();
        _panel.name = "Panel";
        _panel.tag = "MainPanel";
        _panelBox = _panel.AddComponent<BoxCollider2D>();
        _panelBox.offset = new Vector2(0f,0f);
        _panelBox.size = new Vector2(6f, 6f);
        _centerPoint = new GameObject();
        _centerPoint.name = "Letters";
        _centerPoint.tag = "Center";
        _centerPoint.AddComponent<NewOnEmptyCreation>();
        _centerPoint.AddComponent<OnStartAnimation>();
        // panel Creator & Center Point Creation Ends
        for (var row = 0; row < gridCreator.gridSize; row++)
        {
            for (var col = 0; col < gridCreator.gridSize; col++)
            {
                var key = gridCreator.GetPlayerPrefsKey(row, col);
                gridCreator.boxSelected[row, col] = PlayerPrefs.GetInt(key) == 1;
            }
        }
        size = _panelBox.size;
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
                        woodPanel.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        woodPanel.tag = "WoodPanel";
                        woodPanel.name = (col + (row * gridSize) + 1).ToString(); //  <= Formula
                        woodPanel.transform.localScale = new Vector3(width/(gridCreator.gridSize * _sizeMultiplyer), height / (gridCreator.gridSize * _sizeMultiplyer), 0f) ;
                        _HoffSet = new Vector3(woodPanel.transform.localScale.x + _offSetVar, 0f, 0f);
                        _VoffSet = new Vector3(-(woodPanel.transform.localScale.x + _offSetVar) * gridCreator.gridSize, -(woodPanel.transform.localScale.y + _offSetVar), 0f);
                        gameObject.transform.position += _HoffSet;
                    }
                    else
                    {
                        gameObject.transform.position += _HoffSet;
                    }
                }
                gameObject.transform.position += _VoffSet;
            }
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("WoodPanel");
            float _maxX = -100;
            float _minX = 100;
            float _maxY = -100;
            float _minY = 100;
            foreach (GameObject item in objectsWithTag)
            {
                if (item.transform.position.x > _maxX)
                {
                    _maxX = item.transform.position.x;
                }
                if (item.transform.position.x < _minX)
                {
                    _minX = item.transform.position.x;
                }
                if (item.transform.position.x > _maxY)
                {
                    _maxY = item.transform.position.x;
                }
                if (item.transform.position.x < _minY)
                {
                    _minY = item.transform.position.x;
                }
            }
            Debug.Log("MinX : " + _minX + " MaxX : " + _maxX);
            Debug.Log("MinY : " + _minY + " MaxY : " + _maxY);
            Vector3 _center = new Vector2((_maxX + _minX) / 2 , (_maxY + _minY) / 2);
            Debug.Log("Center : " + _center );
            _centerPoint.transform.position = _center;
            foreach (GameObject item in objectsWithTag)
            {
                item.transform.SetParent(_centerPoint.transform);
            }
            //_centerPoint.transform.position = Vector3.zero;
        }
        EditorUtility.SetDirty(gridCreator);
    }
}