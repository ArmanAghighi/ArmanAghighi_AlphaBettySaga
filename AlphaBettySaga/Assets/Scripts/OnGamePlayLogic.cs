using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
public class OnGamePlayLogic : MonoBehaviour
{
    private bool _gameOver = false;
    [SerializeField] private string _name;
    [SerializeField] private int _value;
    [SerializeField] private GameObject _explostionParticleSystem;
    [SerializeField] private GameObject _foundBeforePanel;
    [SerializeField] private GameObject _branchPrefab;
    private Transform _foundBeforePanelTransform;
    private SpriteRenderer _spriteRenderer;
    private static bool _isCorrectWord = false;
    private static bool _foundWord = false;
    private static int _score = 0;
    private static int _allValue = 0;
    private static GameObject _lastObject = null;
    private bool _isSelected = false;
    private Text _showText;
    private Text _scoreText;
    private Text _remainMoveText;
    private Text _onMoveScore;
    private Text _scoreToPass;
    private int _remainMove = 8;
    private List<string> _words = new List<string>();
    private void Awake()
    {
        _foundBeforePanelTransform = GameObject.FindGameObjectWithTag("FoundPanel").GetComponent<Transform>();
        _showText = GameObject.FindGameObjectWithTag("Show-Word").GetComponent<Text>();
        _scoreText = GameObject.FindGameObjectWithTag("Score-Word").GetComponent <Text>();
        _remainMoveText = GameObject.FindGameObjectWithTag("RemainMoveText").GetComponent<Text>();
        _onMoveScore = GameObject.FindGameObjectWithTag("OnMoveScore").GetComponent<Text>();
        _scoreToPass= GameObject.FindGameObjectWithTag("ScoreToPass").GetComponent<Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _spriteRenderer.sortingOrder = 2;
    }
    private void Update()
    {
        if (gameObject.tag == "isSelected")
        {
            Destroy(gameObject);
        }
        if (_remainMove > 0 && !_gameOver)
        {
            OnGamePlayManager();
        }
        if(_remainMove <= 0)
        {
            if (int.Parse(_scoreText.text) < int.Parse(_scoreToPass.text))
            {
                _gameOver = true;
            }
            else
            {
                //Victory
            }
        }

        

    }
    
    private void OnGamePlayManager()
    {
        if (Input.GetMouseButton(0))
        {
                OnSelectManager();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_showText.text.Length >= 3)
            {
                if (CheckCorrectWord(_showText.text))
                {
                    _isCorrectWord = true;

                }
                else
                {
                    _isCorrectWord = false;
                }

            }
            else
            {
                _isCorrectWord = false;
            }
            OnDeselectManager();
        }
    }
    private void OnSelectManager()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject && _showText.text.Length <= 7)
        {
            if (!_isSelected)
            {
                if (_lastObject == null)
                {
                   _foundWord = false;
                    gameObject.tag = "isSelected";
                    _lastObject = transform.parent.gameObject;
                    _spriteRenderer.color = Color.green;
                    _showText.text += _name;
                    _isSelected = true;
                    if (_name != "")
                    {
                        _allValue += _value;
                    }
                    _onMoveScore.text = (_allValue * _showText.text.Length * 10).ToString();
                }
                if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 1)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 1)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 5)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 5)|| (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 4)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 4)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 6)||(int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 6))
                {
                    gameObject.tag = "isSelected";
                    _lastObject = transform.parent.gameObject;
                    _spriteRenderer.color = Color.green;
                    _showText.text += _name;
                    _isSelected = true;
                    if (_name != "")
                    {
                        _allValue += _value;
                    }
                    _onMoveScore.text = (_allValue * _showText.text.Length * 10).ToString();
                }
            }
        }
    }
    private void OnDeselectManager()
    {
        if (!_isCorrectWord)
        {
            if (gameObject.tag == "isSelected")
                gameObject.tag = "Untagged";
            _lastObject = null;
            _allValue = 0;
            _spriteRenderer.color = Color.white;
        }
        else
        {
            _score += _allValue * _showText.text.Length * 10;
            _scoreText.text = _score.ToString();
            _remainMove--;
            _remainMoveText.text = _remainMove.ToString();
            _words.Add(_showText.text);
        }
        _onMoveScore.text = 0.ToString();
        _isSelected = false;
        _showText.text = "";
    }
    private bool CheckCorrectWord(string _newName)
    {
        string filePath = "D:/Git/ArmanAghighi_AlphaBettySaga/AlphaBettySaga/Assets/Resources/Resource.txt";
        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length == _newName.Length && line.ToLower().Trim().Contains(_newName.ToLower().Trim()))
                {
                    foreach (var word in _words)
                    {
                        if (_showText.text == word)
                        {
                            GameObject Panel = Instantiate(_foundBeforePanel, _foundBeforePanelTransform.position, Quaternion.identity);
                            _foundWord = true;
                            return false;
                        }
                    }
                    reader.Close();
                    if(!_foundWord)
                    {
                        return true;
                    }
                }
            }
            reader.Close();
            return false;
        }
        else
        {
            return false;
        }
    }
}