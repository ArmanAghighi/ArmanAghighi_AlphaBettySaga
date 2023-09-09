using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShuffleTime : MonoBehaviour
{
    public bool _isAccessable = false;
    private Scrollbar _scrollBar;
    private bool _startShuffling = false;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] AnimationClip _shuffleClip;
    private PauseScript _pauseScript;
    private void Awake()
    {
        _scrollBar = GameObject.FindGameObjectWithTag("Scroll").GetComponent<Scrollbar>();
        _scrollBar.size = 0f;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (_startShuffling)
        {
            StartCoroutine(ShuffleAnimatio());
        }
        if (!_isAccessable)
        {
            float newSize = Mathf.PingPong(Time.time / 10, 1f);
            _scrollBar.size = newSize;
            if (newSize >= 0.99)
                _isAccessable = true;
        }
    }
    public void StartShuffling()
    {
        if (_isAccessable)
        {
            _startShuffling = true;
            _scrollBar.size = 0;
            _isAccessable = false;
        }
    }
    IEnumerator ShuffleAnimatio()
    {
        _animator.SetTrigger("Start_Shuffle");
        _spriteRenderer.sortingOrder = 3;
        _startShuffling = false;
        yield return new WaitForSeconds(_shuffleClip.length);
        _spriteRenderer.sortingOrder = -2;
    }
}