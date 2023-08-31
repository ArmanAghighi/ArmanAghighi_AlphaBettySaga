using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleTime : MonoBehaviour
{
    private int _shuffleTime = 45;
    private bool _startShuffling = false;
    private bool _animationIsStarted = false;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] AnimationClip _shuffleClip;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (_startShuffling)
        {
            StartCoroutine(ShuffleAnimatio());
        }
    }
    public void StartShuffling()
    {
        _startShuffling = true;
    }
    IEnumerator ShuffleAnimatio()
    {
        _animator.SetTrigger("Start_Shuffle");
        _spriteRenderer.sortingOrder = 3;
        _animationIsStarted = true;
        _startShuffling = false;
        yield return new WaitForSeconds(_shuffleClip.length);
        _spriteRenderer.sortingOrder = -2;
        _animationIsStarted = false;
    }
}
