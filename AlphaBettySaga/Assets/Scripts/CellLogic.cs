using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellLogic : MonoBehaviour
{
    private Scrollbar _scrollBar;
    private bool _isAccess = false;
    private bool[] _isFull;
    [SerializeField] GameObject[] _alphabets;
    [SerializeField] GameObject[] _panelCells;
    [SerializeField] GameObject[] _vowelAlphabets;
    [SerializeField] int _ScoreToPass;
    GameObject[] _words = new GameObject[25];
    private Text _scoreToPassText;
    private bool _isSetup = false;
    private void Awake()
    {
        _scoreToPassText = GameObject.FindGameObjectWithTag("ScoreToPass").GetComponent<Text>();
        _scrollBar = GameObject.FindGameObjectWithTag("Scroll").GetComponent<Scrollbar>();
        _scoreToPassText.text = "/ " + _ScoreToPass.ToString();
    }
    private void Update()
    {
        if (!_isSetup)
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
                    if (i % 3 == 0)
                    {
                        int _selectAlphabet = Random.Range(0, _vowelAlphabets.Length);
                        _words[i] = Instantiate(_vowelAlphabets[_selectAlphabet], _panelCells[i].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        int _selectAlphabet = Random.Range(0, _alphabets.Length);
                        _words[i] = Instantiate(_alphabets[_selectAlphabet], _panelCells[i].transform.position, Quaternion.identity);
                    }
                    _isFull[i] = true;
                    _words[i].transform.localScale = Vector3.zero;
                    _words[i].transform.SetParent(GameObject.Find((i + 1).ToString()).gameObject.transform);
                }
            }
            _isSetup = true;
        }
        if (_scrollBar.size >= 0.99)
        {
            _isAccess = true;
        }
        
    }
    public void NewSetup()
    {
        if (_isAccess)
        {
            for (int i = 0; i < _panelCells.Length; i++)
            {
                Destroy(_words[i]);
            }
            _isSetup = false;
            _isAccess = false;
        }
    }
}
