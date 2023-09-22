using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Size : MonoBehaviour
{
    private int desiredWidth = 820;
    private int desiredHeight = 980;

    void Start()
    {
        StartCoroutine(SetResolutionCoroutine());
    }

    IEnumerator SetResolutionCoroutine()
    {
        yield return new WaitForEndOfFrame();

        Screen.SetResolution(desiredWidth, desiredHeight, FullScreenMode.Windowed);
    }
}