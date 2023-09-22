using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellLogic : MonoBehaviour
{
    GameObject[] _panelCells;
    [SerializeField] GameObject _emptyLetter;
    GameObject[] _words;
    private Text _scoreToPassText;
    private Text _remainMove;
    private bool _isSetup = false;
    public int screenWidth = 1360;
    public int screenHeight = 1640;
    private bool[] _isFull;
    private void Start()
    {
        SetScreenResolution();
    }
    private void SetScreenResolution()
    {
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
    private void Awake()
    {
        _scoreToPassText = GameObject.FindGameObjectWithTag("ScoreToPass").GetComponent<Text>();
        _remainMove = GameObject.FindGameObjectWithTag("RemainMoveText").GetComponent<Text>();
        LevelManager _levelManager = GameObject.FindGameObjectWithTag("PanelCreator").GetComponent<LevelManager>();
        _scoreToPassText.text = _levelManager._score.ToString();
        _remainMove.text = _levelManager._move.ToString();
        _panelCells = GameObject.FindGameObjectsWithTag("WoodPanel");
        _words = new GameObject[_levelManager.gridCreator.gridSize];
    }
    private void Update()
    {
/*        if (!_isSetup)
        {
            _isFull = new bool[_panelCells.Length];
            for (int i = 0; i < _panelCells.Length; i++)
            {
                _isFull[i] = false;
            }
            for (int i = 0; i < _panelCells.Length; i++)
            {
                if (_isFull[i] == false)
                {

                    _words[i] = Instantiate(_emptyLetter, _panelCells[i].transform.position, Quaternion.identity);
                    _isFull[i] = true;
                    _words[i].transform.localScale = Vector3.zero;
                    _words[i].transform.SetParent(GameObject.Find((i + 1).ToString()).gameObject.transform);
                }
            }
            _isSetup = true;
        }
*/
    }
}
