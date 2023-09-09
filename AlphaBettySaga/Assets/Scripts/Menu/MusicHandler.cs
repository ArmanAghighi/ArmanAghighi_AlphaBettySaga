using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _fisrtAudioSource;
    [SerializeField] private AudioSource _secondeAudioSource;
    [SerializeField] private GameObject _musicGameObject;
    private bool _isMute = false;
    private SpriteRenderer _MusicSpriteRenderer;
    private const string _muteKey = "IsMute";
    private void Awake()
    {
        _MusicSpriteRenderer = _musicGameObject.GetComponent<SpriteRenderer>();
        if (PlayerPrefs.HasKey(_muteKey))
        {
            if (PlayerPrefs.GetInt(_muteKey) == 1)
            {
                _MusicSpriteRenderer.color = Color.green;
                _isMute = true;
            }
            else
            {
                _MusicSpriteRenderer.color = Color.red;
                _isMute = false;
            }
        }
        else
        {
            _MusicSpriteRenderer.color = Color.green;
            _isMute = true;
        }
        OnMuteClick();
    }
    public void OnMuteClick()
    {
        if (!_isMute)
        {
            _fisrtAudioSource.enabled = false;
            _secondeAudioSource.enabled = false;
            _isMute = true;
            _MusicSpriteRenderer.color = Color.red;
        }
        else if (_isMute)
        {
            _fisrtAudioSource.enabled = true;
            _secondeAudioSource.enabled = true;
            _isMute = false;
            _MusicSpriteRenderer.color = Color.green;
        }
    }
    public void SaveSetting()
    {
        if (_isMute)
        {
            PlayerPrefs.SetInt(_muteKey, 0);
        }
        else
        {
            PlayerPrefs.SetInt(_muteKey, 1);
        }
        Debug.Log("Saved");
        PlayerPrefs.Save();
    }
    public void DefaultComponySetting()
    {
        PlayerPrefs.SetInt(_muteKey, 1);
        Debug.Log("Default");
        _fisrtAudioSource.enabled = true;
        _secondeAudioSource.enabled = true;
        _isMute = false;
        _MusicSpriteRenderer.color = Color.green;
        PlayerPrefs.Save();
    }
}
