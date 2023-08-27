using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    private Rigidbody2D _stoneRigidBody;
    private Animator _stoneAnimator;
    private void Awake()
    {
        _stoneRigidBody = GameObject.Find("SettingParent").GetComponent<Rigidbody2D>();
        _stoneAnimator = GameObject.Find("SettingParent").GetComponent<Animator>();
        _stoneRigidBody.gravityScale = 0;
        _stoneAnimator.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape");
            _stoneRigidBody.gravityScale = 0;
            _stoneAnimator.enabled = true;
            _stoneAnimator.SetTrigger("Exit");
        }
    }
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingClick()
    {
        _stoneRigidBody.gravityScale = 1;
        _stoneAnimator.enabled = false;
    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
