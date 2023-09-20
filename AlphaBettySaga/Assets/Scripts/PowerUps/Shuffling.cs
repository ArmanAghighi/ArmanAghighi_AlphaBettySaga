using UnityEngine;
using DG.Tweening;

public class Shuffling : MonoBehaviour
{
    private GameObject[] _objects;
    private Transform[] _parents;
    private int _objectCount;
    private bool _isAccess;
    public bool _isDone;
    public void Shuffle()
    {
        _isAccess = gameObject.transform.GetChild(0).GetComponent<ShuffleTime>()._isAccessable;

        if (_isAccess)
        {
            FindAndReplaceEmpty();
            _objectCount = _objects.Length;
            CacheParents();
            for (int i = 0; i < _objectCount; i++)
            {
                int randomIndex = Random.Range(i, _objectCount);

                GameObject object1 = _objects[i];
                GameObject object2 = _objects[randomIndex];

                SwapPositions(object1, object2);
                SwapParents(object1, object2);
            }
            _isDone = true;
        }
    }

    private void FindAndReplaceEmpty()
    {
        _objects = GameObject.FindGameObjectsWithTag("Letter");
    }

    private void CacheParents()
    {
        _parents = new Transform[_objectCount];
        for (int i = 0; i < _objectCount; i++)
        {
            _parents[i] = _objects[i].transform.parent;
        }
    }

    private void SwapPositions(GameObject object1, GameObject object2)
    {
        Vector3 tempPosition = object1.transform.position;
        object1.transform.DOMove(object2.transform.position, 1f);
        object2.transform.DOMove(tempPosition, 1f);
    }

    private void SwapParents(GameObject object1, GameObject object2)
    {
        Transform tempParent = object1.transform.parent;
        object1.transform.SetParent(object2.transform.parent);
        object2.transform.SetParent(tempParent);
    }
}