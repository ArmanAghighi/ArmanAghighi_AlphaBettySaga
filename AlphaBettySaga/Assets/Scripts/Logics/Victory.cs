using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject _victory;
    [SerializeField] private Transform _victoryPos;
    [SerializeField] private GameObject _victoryMissle;
    private bool _isVictory = false;
    private int _remainMoveGameObject = 0;
    private int _scoreToPass;
    private void Awake()
    {
        _scoreToPass = GameObject.FindGameObjectWithTag("PanelCreator").GetComponent<LevelManager>()._score;
    }
    private void Update()
    {
        if ((NewLogicSystem._score >= _scoreToPass && !_isVictory) || (NewLogicSystem._remainMove <= 0 && !_isVictory))
        {
            for (int i = 0; i < NewLogicSystem._remainMove - 1; i++)
            {
                Instantiate(_victoryMissle);
            }
            StartCoroutine(VictoryAnimation());
        }
    }
    private IEnumerator VictoryAnimation()
    {
        _isVictory = true;
        yield return new WaitForSeconds(3f);
        _remainMoveGameObject = NewLogicSystem._remainMove;
        for (int i = 0; i < _remainMoveGameObject; i++)
        {
            Instantiate(_victory, _victoryPos.position, Quaternion.identity);
        }
        GameObject _parent = _victory.transform.GetChild(0).gameObject;
        if (NewLogicSystem._score < _scoreToPass / 8)
        {
            _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (NewLogicSystem._score < _scoreToPass / 4)
        {
            _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

        }
        else if (NewLogicSystem._score < _scoreToPass / 2)
        {
            _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        }
        NewLogicSystem._gameOver = true;
    }
}
