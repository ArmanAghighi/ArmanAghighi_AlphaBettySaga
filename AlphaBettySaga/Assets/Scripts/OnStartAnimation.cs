using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnStartAnimation : MonoBehaviour
{
    private bool _isStarted = false;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private string _name;
    [SerializeField] private int _value;
    private bool _isCorrectWord = false;
    private Text _showText;
    private bool _isSelected = false;
    private void Awake()
    {
        _showText = GameObject.FindGameObjectWithTag("Show-Word").GetComponent<Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        _spriteRenderer.sortingOrder = 2;
    }
    private void Update()
    {
        OnGamePlayManager();
        if (!_isStarted)
        {
            StartCoroutine(Animation());
            _isStarted = true;
        }
    }
    private IEnumerator Animation()
    {

            float _duration = 5f;
            float _elapsedTime = 0f;
            Vector3 _targetScale = new Vector3(1.8f, 1.8f, 1.8f);
            while (_elapsedTime < _duration)
            {
                float t = _elapsedTime / _duration;
                gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, _targetScale, t);
                _elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameObject.transform.localScale = _targetScale;
    }
    private void OnGamePlayManager()
    {
        if (Input.GetMouseButton(0))
        {
            OnSelectManager();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnDeselectManager();
        }
    }
    private void OnSelectManager()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            _spriteRenderer.color = Color.green;
            if(!_isSelected)
            {
                _showText.text += _name;
                _isSelected = true;
            }
        }
        else
        {
            _isSelected = false;
        }
    }
    private void OnDeselectManager()
    {
        if (!_isCorrectWord)
        {
            _spriteRenderer.color = Color.white;
            _isSelected = false;
            _showText.text = "";
        }
    }
}
