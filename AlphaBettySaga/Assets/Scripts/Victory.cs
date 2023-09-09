using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    [SerializeField] private GameObject _victory;
    [SerializeField] private Transform _victoryPos;
    private bool _isVictory = false;
    private void Update()
    {
        if ((NewLogicSystem._score >= 2100 && !_isVictory) || (NewLogicSystem._remainMove <= 7 && !_isVictory))
        {
            Instantiate(_victory,_victoryPos.position,Quaternion.identity);
            GameObject _parent = _victory.transform.GetChild(0).gameObject;
            if (NewLogicSystem._score < 1200)
            {
                _parent.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                
            }
            else if (NewLogicSystem._score < 1800)
            {
                _parent.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                _parent.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
            }
            _isVictory = true;
        }
    }
}
