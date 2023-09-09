using UnityEngine;
using DG.Tweening;

public class StoneTurning : MonoBehaviour
{
    [SerializeField]private float rotationSpeed = 180f; // The speed of rotation in degrees per second

    private void Start()
    {
        // Start the self rotation animation
        StartSelfRotation();
    }

    private void StartSelfRotation()
    {
        transform.DORotate(new Vector3(0f, 0f, 360f), rotationSpeed, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}

