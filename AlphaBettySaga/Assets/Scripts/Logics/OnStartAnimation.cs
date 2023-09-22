using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartAnimation : MonoBehaviour
{
    private bool _isStarted = false;
    private void Update()
    {
        if (!_isStarted)
        {
            GameObject[] letterObjects = GameObject.FindGameObjectsWithTag("Letter");

            if (letterObjects.Length!= 0)
            {
                foreach (GameObject letterObj in letterObjects)
                {
                    StartCoroutine(Animation(letterObj));
                }
            }
            _isStarted = true;
        }
    }
    private IEnumerator Animation(GameObject obj)
    {
        float _duration = 1f;
        float _elapsedTime = 0f;
        float _targetScaleSize = 1.8f;
        Vector3 _targetScaleVector = new Vector3(_targetScaleSize, _targetScaleSize, _targetScaleSize);
        Vector3 _initialScale = obj.transform.localScale;

        while (_elapsedTime < _duration)
        {
            float t = _elapsedTime / _duration;
            obj.transform.localScale = Vector3.Lerp(_initialScale, _targetScaleVector, t);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = _targetScaleVector;
    }
}