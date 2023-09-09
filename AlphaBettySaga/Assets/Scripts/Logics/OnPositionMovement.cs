using UnityEngine;
using DG.Tweening;
public class OnPositionMovement : MonoBehaviour
{
    private Transform _newParentTransform;
    private bool _isMoving = false;
    public float duration = 1f;
    private void OnTransformParentChanged()
    {
        _isMoving = true;
    }
    private void Update()
    {
        if (_isMoving)
        {
            _newParentTransform = transform.parent;

            MoveToParent();
        }
    }

    private void MoveToParent()
    {
        gameObject.transform.DOMove(_newParentTransform.position, duration);
        if (gameObject.transform.position == _newParentTransform.position)
        {
            _isMoving = false;
        }
    }
}