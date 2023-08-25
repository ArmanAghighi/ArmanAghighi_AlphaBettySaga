using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CellLogic : MonoBehaviour
{ 
    [SerializeField] GameObject[] _panelCells;
    private bool[] _isFull;
    [SerializeField] GameObject[] _alphabets;
    [SerializeField] GameObject[] _vowelAlphabets;
    GameObject[] _words = new GameObject[25];
    private bool _isSetup = false;
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
    }
    public void NewSetup()
    {
        for (int i = 0; i < _panelCells.Length; i++)
        {
            Destroy(_words[i]);
        }
        _isSetup = false;
    }
}
