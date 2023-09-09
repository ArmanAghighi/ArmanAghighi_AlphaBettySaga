using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEmptyCreation : MonoBehaviour
{
    private GameObject[] _letterPanels = new GameObject[25];
    [SerializeField] private GameObject _panel;

    private void Awake()
    {
        for (int i = 0; i < 25; i++)
        {
            _letterPanels[i] = gameObject.transform.GetChild(i).gameObject;
            _letterPanels[i].tag = "WoodPanel";
        }
    }
    private void Update()
    {

        foreach (GameObject item in _letterPanels)
        {
            if (item.transform.childCount == 0)
            {
                item.tag = "isEmpty";
                Creation(item);
            }
        }
    }
    private void Creation(GameObject item)
    {
        if (int.Parse(item.transform.name) <= 5)
        {
            GameObject _newPanel = Instantiate(_panel, item.transform.position, Quaternion.identity);
            Vector3 _targetScale = new Vector3(1.4f, 1.6f, 1f);
            _newPanel.transform.localScale = _targetScale;
            _newPanel.transform.SetParent(item.transform);
            item.tag = "WoodPanel";
        }
        else
        {
            GameObject _lastEmptyPanel = gameObject.transform.GetChild(int.Parse(item.transform.name) - 6).gameObject;
            _lastEmptyPanel.transform.GetChild(0).SetParent(item.transform);

        }
    }
}
