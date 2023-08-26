using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnSettingClick()
    {

    }
    public void OnQuitClick()
    {
        Application.Quit();
    }
}
