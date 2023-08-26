using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdProperty : MonoBehaviour
{
    private float _birdSpeed = 3f;
    void Update()
    {
        transform.Translate(Vector3.right * _birdSpeed * Time.deltaTime);
    }
}
