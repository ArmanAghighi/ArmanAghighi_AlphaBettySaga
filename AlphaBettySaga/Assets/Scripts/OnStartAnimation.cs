using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartAnimation : MonoBehaviour
{
    private bool _isStarted = false;
    private bool _isShuffle = false;
    private int _childCount;
    private void Awake()
    {
        _childCount = gameObject.transform.childCount;
    }
    private void Update()
    {
        if (!_isStarted || _isShuffle)
        {
            GameObject[] letterObjects = GameObject.FindGameObjectsWithTag("Letter");

            if (letterObjects.Length!= 0)
            {
                foreach (GameObject letterObj in letterObjects)
                {
                    StartCoroutine(Animation(letterObj));
                }
                _isStarted = true;
                _isShuffle = false;
            }
        }
    }
    private IEnumerator Animation(GameObject obj)
    {
        float _duration = 1f;
        float _elapsedTime = 0f;
        Vector3 _targetScale = new Vector3(1.8f, 1.8f, 1.8f);
        Vector3 _initialScale = obj.transform.localScale;

        while (_elapsedTime < _duration)
        {
            float t = _elapsedTime / _duration;
            obj.transform.localScale = Vector3.Lerp(_initialScale, _targetScale, t);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = _targetScale;
    }
    public void ChangeShuffle()
    {
        _isShuffle = true;
    }
}