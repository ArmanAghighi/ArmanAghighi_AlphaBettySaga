using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalProperty : MonoBehaviour
{
    private float _birdSpeed;
    GameObject _destroyObject;
    private void Awake()
    {
        _destroyObject = GameObject.Find("DestroyGameObject");
        _birdSpeed = Random.Range(2, 4);
    }
    void Update()
    {
        if (gameObject.tag != "Monkey")
        {
            if (gameObject.tag == "Frog")
                transform.Translate(Vector3.left * _birdSpeed * Time.deltaTime);
            else
                transform.Translate(Vector3.right * _birdSpeed * Time.deltaTime);
            if (Mathf.Abs(gameObject.transform.position.x) >= Mathf.Abs(_destroyObject.transform.position.x))
                Destroy(gameObject);
        }
    }
}
