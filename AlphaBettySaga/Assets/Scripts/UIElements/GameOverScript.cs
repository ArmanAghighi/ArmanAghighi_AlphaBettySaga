using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{
    private int _movementCount;
    private int newSortingOrder = 20;
    private void Awake()
    {        
        LevelManager _levelManager = GameObject.FindGameObjectWithTag("PanelCreator").GetComponent<LevelManager>();
        _movementCount = _levelManager._move;
    }
    private void Update()
    {
        _movementCount = int.Parse(GameObject.FindGameObjectWithTag("RemainMoveText").GetComponent<Text>().text);
        if (_movementCount <= 0)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.sortingOrder = newSortingOrder;
            ChangeChildrenSortingOrder(transform, newSortingOrder);
            Time.timeScale = 0;
        }
    }
    private void ChangeChildrenSortingOrder(Transform parentTransform, int sortingOrder)
    {
        foreach (Transform child in parentTransform)
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.sortingOrder = sortingOrder;
            }
            ChangeChildrenSortingOrder(child, sortingOrder);
        }
    }
}
