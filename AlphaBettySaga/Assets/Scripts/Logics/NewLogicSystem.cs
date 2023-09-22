using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;
public class NewLogicSystem : MonoBehaviour
{
    //===================================
    private int _indexValue;
    private static bool _isGold = false;
    private bool _isOnPause = false;
    //===================================
    public static bool _gameOver = false;
    private string _name;
    private int _value = 0;
    [SerializeField] private GameObject _explostionParticleSystem;
    [SerializeField] private GameObject _foundBeforePanel;
    [SerializeField] private GameObject _branchPrefab;
    private Transform _foundBeforePanelTransform;
    private SpriteRenderer _spriteRenderer;
    private static bool _isCorrectWord = false;
    private static bool _foundWord = false;
    public static int _score = 0;
    private static int _allValue = 0;
    private static GameObject _lastObject = null;
    private bool _isSelected = false;
    private Text _showText;
    private Text _scoreText;
    private Text _remainMoveText;
    private Text _onMoveScore;
    public static int _remainMove = 8;
    private static int _branchForm;
    private List<string> _words = new List<string>();
    private List<string> _list = new List<string>();
    private void Awake()
    {
        _foundBeforePanelTransform = GameObject.FindGameObjectWithTag("FoundPanel").GetComponent<Transform>();
        _showText = GameObject.FindGameObjectWithTag("Show-Word").GetComponent<Text>();
        _showText.text = "";
        _scoreText = GameObject.FindGameObjectWithTag("Score-Word").GetComponent<Text>();
        if (_scoreText.text == "")
        {
            _scoreText.text = 0.ToString();
        }
        _remainMoveText = GameObject.FindGameObjectWithTag("RemainMoveText").GetComponent<Text>();
        _onMoveScore = GameObject.FindGameObjectWithTag("OnMoveScore").GetComponent<Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _spriteRenderer.sortingOrder = 2;
        _indexValue = gameObject.GetComponent<OnEmptyLetterValueSelection>()._valueIndex;
    }
    private void Update()
    {
        _isOnPause = PauseScript._onPause;
        if (!_isOnPause && !_gameOver)
        {
            if (_name == null && _value == 0)
            {
                _name = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text;
                _value = int.Parse(gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text);
                if (_indexValue == 1)
                {
                    _value *= 2;
                }
            }

            if (_remainMove > 0)
            {
                OnGamePlayManager();
            }
        }
        else
        {
            OnDeselectManager();
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
                if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 1) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 1) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 5) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 5) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 4) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 4) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 6) || (int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 6))
                {
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 5))
                    {
                        _branchForm = 58;//Up
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 4))
                    {
                        _branchForm = 59;//UpRight
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 1))
                    {
                        _branchForm = 56;//Right
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 6))
                    {
                        _branchForm = 53;//DownRight
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 5))
                    {
                        _branchForm = 52;//Down
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) + 4))
                    {
                        _branchForm = 51;//DownLeft
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 1))
                    {
                        _branchForm = 54;//Left
                    }
                    if ((int.Parse(transform.parent.gameObject.name) == int.Parse(_lastObject.name) - 6))
                    {
                        _branchForm = 57;//UpLeft
                    }
                    ChangeID(_branchForm);
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
                if (_indexValue == 2)
                {
                    _isGold = true;
                }
            }
        }

    }
    private void OnDeselectManager()
    {
        if (!_isCorrectWord)
        {
            _isGold = false;
            if (gameObject.tag == "isSelected")
                gameObject.tag = "Letter";
            _lastObject = null;
            _allValue = 0;
            if (_indexValue == 0)
            {
                _spriteRenderer.color = Color.white;
            }
            else if(_indexValue == 1)
            {
                _spriteRenderer.color = Color.gray;
            }
            else
            {
                _spriteRenderer.color = Color.yellow;
            }
        }
        else
        {
            DestroyObjectsWithTag("isSelected");
            if (_isGold)
            {
                _score += _allValue * _showText.text.Length * 10 * 2;
            }
            else
            {
                _score += _allValue * _showText.text.Length * 10;
            }
            _scoreText.text = _score.ToString();
            _isGold = false;
            _remainMove--;
            _remainMoveText.text = _remainMove.ToString();
            _words.Add(_showText.text);
        }
        _onMoveScore.text = 0.ToString();
        _isSelected = false;
        _showText.text = "";
        DestroyObjectsWithTag("Branch");
    }
    private bool CheckCorrectWord(string _newName)
    {
        string filePath = "D:/Git/ArmanAghighi_AlphaBettySaga/AlphaBettySaga/Assets/Resources/Resource.txt";
        if (File.Exists(filePath) && _list.Count == 0)
        {
            StreamReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                _list.Add(line.ToLower().Trim());
            }
        }
        _newName = _newName.ToLower().Trim();
        if (!string.IsNullOrEmpty(_list.Where(item => item == _newName).FirstOrDefault()))
        {
            if (!string.IsNullOrEmpty(_words.Where(item => item == _newName).FirstOrDefault()))
            {
                GameObject Panel = Instantiate(_foundBeforePanel, _foundBeforePanelTransform.position, Quaternion.identity);
                _foundWord = false;
            }
            else
                _foundWord = true;
        }
        else
            _foundWord = false;
        return _foundWord;

    }
    private void ChangeID(int Form)
    {
        GameObject _branch = Instantiate(_branchPrefab, _lastObject.transform.position, Quaternion.identity);
        _branch.tag = "Branch";
        switch (Form)
        {
            case 58:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case 59:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
                break;
            case 56:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 53:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 315f);
                break;
            case 52:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                break;
            case 51:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 225f);
                break;
            case 54:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case 57:
                _branch.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
                break;
            default:
                break;
        }
    }
    private void DestroyObjectsWithTag(string tag)
    {
        GameObject[] _objects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in _objects)
        {
            Instantiate(_explostionParticleSystem, obj.transform.position, Quaternion.identity);
            Destroy(obj);
        }
    }
}