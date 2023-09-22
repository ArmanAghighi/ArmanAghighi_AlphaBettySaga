using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewOnEmptyCreation : MonoBehaviour
{
    private GameObject[] _letterPanels;
    [SerializeField] private GameObject _panel;
    private bool _startGame = false;
    private int _size = 0;
    public int GetSize()
    {
        GridCreator specificInstance = Resources.Load<GridCreator>("GridCreatorLevel1");
        _size = specificInstance.gridSize;
        return _size;
    }
    private void Start()
    {
        int _numberOfChild = gameObject.transform.childCount;
        
        if (_numberOfChild > 0)
        {
            _startGame = true;
            _letterPanels = new GameObject[_numberOfChild];
        }
        if (_startGame)
        {
            for (int i = 0; i < _numberOfChild; i++)
            {
                _letterPanels[i] = gameObject.transform.GetChild(i).gameObject;
                _letterPanels[i].tag = "WoodPanel";
            }
        }
    }
    private void Update()
    {
        foreach (GameObject item in _letterPanels)
        {
            if (item.transform.childCount == 0 && _startGame)
            {
                item.tag = "isEmpty";
                Creation(item , item.transform);
            }
        }
    }
    private void Creation(GameObject item , Transform transform)
    {
        GameObject _newPanel = Instantiate(_panel, transform.position, transform.rotation);
        Vector3 _targetScale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, 1f);
        _newPanel.transform.localScale = _targetScale;
        _newPanel.transform.SetParent(transform);
        item.tag = "WoodPanel";
    }
}
