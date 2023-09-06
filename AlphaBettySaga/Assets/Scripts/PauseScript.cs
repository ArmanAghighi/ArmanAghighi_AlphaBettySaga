using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool _onPause = false;
    [SerializeField] GameObject _pausePanel;
    public void OnPausGame()
    {
        _onPause = !_onPause;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausGame();
        }
        if (_onPause)
        {
            Time.timeScale = 0f;
            _pausePanel.GetComponent<SpriteRenderer>().sortingOrder = 10;
            _pausePanel.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
        else
        {
            _pausePanel.GetComponent<SpriteRenderer>().sortingOrder = -10;
            _pausePanel.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -10;
            Time.timeScale = 1f;
        }
    }
}
