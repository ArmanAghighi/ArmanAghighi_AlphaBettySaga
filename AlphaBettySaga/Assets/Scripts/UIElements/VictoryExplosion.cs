using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class VictoryExplosion : MonoBehaviour
{
    [SerializeField] private Transform _onVictoryExplosionTransform;
    [SerializeField] private Animator _animator;
    private float _speed = 3f;
    private Vector3 _offSet = new Vector3(0.1f,0.1f,0.1f);
    private SpriteRenderer _spriteRenderer;
    GameObject[] _letters = new GameObject[26];
    private Transform _destination;
    GameObject _destroyableObject;
    private Text _scoreText;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _spriteRenderer.sortingOrder = 1000;
        _letters = GameObject.FindGameObjectsWithTag("WoodPanel");
        _destination = _letters[Random.Range(0, _letters.Length)].transform;
        _scoreText = GameObject.FindGameObjectWithTag("Score-Word").GetComponent<Text>();
    }

    private void Update()
    {
        Vector3 direction = _destination.position - transform.position;
        transform.position += direction.normalized * _speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, _destination.position) <= _offSet.magnitude)
        {
            _speed = 0f; 
            _animator.SetTrigger("Explosion"); AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            gameObject.transform.localScale = new Vector3(3.0f,3.0f,3.0f);
            if (stateInfo.normalizedTime <= 1f)
            {
                _destroyableObject = _destination.transform.GetChild(0).gameObject;
                NewLogicSystem._score += (int.Parse(_destroyableObject.transform.GetChild(1).GetComponent<TextMeshPro>().text) * 10);
                Destroy(_destroyableObject);
                Destroy(gameObject);
                _scoreText.text = (NewLogicSystem._score).ToString();
            }
        }

    }
}