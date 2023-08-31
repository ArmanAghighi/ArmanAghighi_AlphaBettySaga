using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
public class OnStartAnimation : MonoBehaviour
{
    private bool _isStarted = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private string _name;
    [SerializeField] private int _value;
    private bool _isCorrectWord = false;
    private Text _showText;
    private Text _scoreText;
    private Text _remainMoveText;
    private int _remainMove = 8;
    private bool _isSelected = false;
    private static int _score = 0;
    private static int _allValue = 0;
    private void Awake()
    {
        _showText = GameObject.FindGameObjectWithTag("Show-Word").GetComponent<Text>();
        _scoreText = GameObject.FindGameObjectWithTag("Score-Word").GetComponent <Text>();
        _remainMoveText = GameObject.FindGameObjectWithTag("RemainMoveText").GetComponent<Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerPrefs.SetInt("Score", 0);
    }
    private void Start()
    {
        _spriteRenderer.sortingOrder = 2;
    }
    private void Update()
    {
        OnGamePlayManager();
        if (!_isStarted)
        {
            StartCoroutine(Animation());
            _isStarted = true;
        }
    }
    private IEnumerator Animation()
    {

            float _duration = 5f;
            float _elapsedTime = 0f;
            Vector3 _targetScale = new Vector3(1.8f, 1.8f, 1.8f);
            while (_elapsedTime < _duration)
            {
                float t = _elapsedTime / _duration;
                gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, _targetScale, t);
                _elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.localScale = _targetScale;
    }
    private void OnGamePlayManager()
    {
        if (Input.GetMouseButton(0))
        {
                OnSelectManager();
        }
        if (Input.GetMouseButtonUp(0) && _showText.text.Length >= 3)
        {
            if(_showText.text != "")
            {
                if (CheckCorrectWord(_showText.text))
                    _isCorrectWord = true;
                else
                    _isCorrectWord = false;
            }
            OnDeselectManager();
        }
        if (Input.GetMouseButtonUp(0) && _showText.text.Length < 3)
        {
            _isCorrectWord = false;
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
                _spriteRenderer.color = Color.green;
                _showText.text += _name;
                _isSelected = true;
                gameObject.tag = "isSelected";
                if (_name != "")
                {
                    _allValue += _value;
                }
            }
        }
        else
        {
            _isCorrectWord = false;
        }
    }
  private void OnDeselectManager()
    {
        if (!_isCorrectWord)
        {
            _allValue = 0;
            gameObject.tag = "Untagged";
            _spriteRenderer.color = Color.white;
        }
        else
        {
            _score += _allValue * _showText.text.Length * 10;
            _scoreText.text = _score.ToString();
            Debug.Log(_score);
            _remainMove--;
            _remainMoveText.text = _remainMove.ToString();
        }
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
                // Check if the line length matches the _newName word length
                if (line.Length == _newName.Length && line.ToLower().Trim().Contains(_newName.ToLower().Trim()))
                {
                    Debug.Log("Word Found");
                    reader.Close();
                    return true;
                }
            }
            reader.Close();
            //Debug.Log("Word Not Found");
            return false;
        }
        else
        {
            return false;
        }
    }

}