using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        PlayerPrefs.SetString("NextScene", sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
