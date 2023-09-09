/*using UnityEngine;
using DG.Tweening;
public class Shuffling : MonoBehaviour
{
    private GameObject[] _objects = new GameObject[26];
    private GameObject[] _parent = new GameObject[26];
    GameObject _object1;
    GameObject _object2;
    private void Update()
    {
        FindAndReplaceEmpty();
    }
    public void Shuffle()
    {

        for (int i = 0; i < 25; i++)
        {
            _object1 = _objects[Random.Range(0, _objects.Length)];
            _object2 = _objects[Random.Range(0, _objects.Length)];

            Vector3 tempPosition = _object1.transform.position;
            _object1.transform.DOMove(_object2.transform.position, 1f);
            _object2.transform.DOMove(tempPosition, 1f);


            Transform tempParent = _object1.transform.parent;
            _object1.transform.SetParent(_object2.transform.parent);
            _object2.transform.SetParent(tempParent);
        }
    }
    private void FindAndReplaceEmpty()
    {
        _objects = GameObject.FindGameObjectsWithTag("Letter");
    }
}
*/
using UnityEngine;
using DG.Tweening;

public class Shuffling : MonoBehaviour
{
    private GameObject[] _objects;
    private Transform[] _parents;
    private int _objectCount;

    private void Start()
    {

    }

    public void Shuffle()
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