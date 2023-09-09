using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        PlayParticleSystem();
        Destroy(gameObject,2f);
    }

    private void PlayParticleSystem()
    {
        var mainModule = _particleSystem.main;
        mainModule.playOnAwake = false;
        _particleSystem.Play();
    }
}