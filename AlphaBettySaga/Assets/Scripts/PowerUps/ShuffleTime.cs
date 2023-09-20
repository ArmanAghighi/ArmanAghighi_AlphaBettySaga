using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ShuffleTime : MonoBehaviour
{
    public bool _isAccessable = false;
    private Scrollbar _scrollBar;
    private bool _isDone;
    private void Awake()
    {
        _scrollBar = GameObject.FindGameObjectWithTag("Scroll").GetComponent<Scrollbar>();
        _scrollBar.size = 0f;
    }
    private void Update()
    {
        if (!_isAccessable)
        {
            float newSize = Mathf.PingPong(Time.time / 10, 1f);
            _scrollBar.size = newSize;
            if (newSize >= 0.99)
            {
                _isAccessable = true;
            }
        }
    }
    public void StartShuffling() 
    {
        if (_isAccessable)
        {
            _scrollBar.size = 0;
            _isDone = gameObject.transform.parent.GetComponent<Shuffling>()._isDone;
            _isAccessable = false;
        }
    }
}