using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;

    // Start is called before the first frame update
    void Start()
    {
        string nextScene = PlayerPrefs.GetString("NextScene", "MainManu");

        StartCoroutine(LoadSceneWithDelay(nextScene));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime / 5f;
            progressBar.value = progress;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
