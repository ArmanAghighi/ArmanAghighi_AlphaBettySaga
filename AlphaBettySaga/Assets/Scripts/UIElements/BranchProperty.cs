using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchProperty : MonoBehaviour
{
    [SerializeField] Sprite[] _branchSprites;
    private Animator _animator;
    private void Awake()
    {
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _branchSprites[Random.Range(0 , _branchSprites.Length)];
        _animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    private void OnAnimatorClicked()
    {
        _animator.SetTrigger("BranchTrigger");
    }
    private void Start()
    {
        OnAnimatorClicked();
    }
}
