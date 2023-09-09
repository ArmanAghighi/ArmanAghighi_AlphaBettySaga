using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundWordProperty : MonoBehaviour
{
    private float _fadeDuration = 3f; 
    [SerializeField] private Image _panelImage;
    private Text _foundedWordText;
    private float _currentPanelAlpha;
    private float _currentTextAlpha;
    private float _targetAlpha = 0f;
    //use this bunch of code to controll transparency of "FoundedPanel and Text"
    private void Start()
    {
        gameObject.transform.parent.SetParent(GameObject.Find("HUD").transform);
        gameObject.transform.parent.localScale = Vector3.one;
        if (_panelImage == null)
        {
            _panelImage = GetComponent<Image>();
        }
        _foundedWordText = gameObject.transform.GetChild(0).GetComponent<Text>();
        _currentPanelAlpha = _panelImage.color.a;
        _currentTextAlpha = _foundedWordText.color.a;
    }

    private void Update()
    {
        _currentPanelAlpha = Mathf.MoveTowards(_currentPanelAlpha, _targetAlpha, Time.deltaTime / _fadeDuration);
        Color newPanelColor = _panelImage.color;
        newPanelColor.a = _currentPanelAlpha;
        _panelImage.color = newPanelColor;

        _currentTextAlpha = Mathf.MoveTowards(_currentTextAlpha, _targetAlpha, Time.deltaTime / _fadeDuration);
        Color newTextColor = _foundedWordText.color;
        newTextColor.a = _currentPanelAlpha;
        _foundedWordText.color = newTextColor;

        if (newPanelColor.a<= 0.15)
            Destroy(GameObject.Find("PanelParent(Clone)"));
    }
}
