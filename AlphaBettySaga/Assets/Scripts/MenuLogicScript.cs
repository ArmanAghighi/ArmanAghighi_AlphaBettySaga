using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MenuLogicScript : MonoBehaviour
{
    private float _characterInstantiateTime = 11f;
    private float _FrogInstantiateTime = 19f;
    private float _monkeyInstantiateTime = 26f;
    private Animator _monkeyAnim;
    [SerializeField] GameObject[] _character;
    private void Start()
    {
        StartCoroutine(InstantiaterCo());
        _monkeyAnim = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Animator>();
    }
    IEnumerator InstantiaterCo()
    {
        while (true)
        {
            if (_character[0].tag == "Bird")
            {
                yield return new WaitForSeconds(_characterInstantiateTime);
                int _instantatedIndex = Random.Range(0, _character.Length);
                float _instantatedY = Random.Range(gameObject.transform.position.y - 2, gameObject.transform.position.y + 2);
                Instantiate(_character[_instantatedIndex], new Vector3(gameObject.transform.position.x, _instantatedY, 0), Quaternion.identity);
            }
            if(_character[0].tag == "Frog")
            {
                yield return new WaitForSeconds(_FrogInstantiateTime);
                int _instantatedIndex = Random.Range(0, _character.Length);
                Instantiate(_character[_instantatedIndex], gameObject.transform.position , Quaternion.identity);
            }
            if (_character[0].tag == "Monkey")
            {
                yield return new WaitForSeconds(_monkeyInstantiateTime);
                _monkeyAnim.SetTrigger("MonkeyAnim");
            }
        }
    }
}
