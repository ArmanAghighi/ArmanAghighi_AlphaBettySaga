using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationScript : MonoBehaviour
{
    private int _characterInstantiateTime = 10;
    [SerializeField] GameObject[] _character;
    private void Start()
    {
        StartCoroutine(InstantiaterCo(_characterInstantiateTime));
    }
    IEnumerator InstantiaterCo(int _characterInstantiateTime)
    {
        int _instantatedIndex = Random.Range(0, _character.Length);
        float _instantatedY = Random.Range(gameObject.transform.position.y - 2, gameObject.transform.position.y + 2);
        Instantiate(_character[_instantatedIndex], new Vector3(gameObject.transform.position.x, _instantatedY, 0), Quaternion.identity);
        yield return new WaitForSeconds(_characterInstantiateTime);
    }
}
