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
    private void Update()
    {
        if ((NewLogicSystem._score >= 2100 && !_isVictory) || (NewLogicSystem._remainMove <= 0 && !_isVictory))
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
        Instantiate(_victory, _victoryPos.position, Quaternion.identity);
        GameObject _parent = _victory.transform.GetChild(0).gameObject;
        if (NewLogicSystem._score < 1200)
        {
            _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

        }
        else if (NewLogicSystem._score < 1800)
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
