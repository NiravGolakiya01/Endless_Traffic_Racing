using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject environmentPanel;
    [SerializeField] GameObject InformationPanel;
    [SerializeField] GameObject PlayButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnvironmentPanel()
    {
        environmentPanel.SetActive(true);
        PlayButton.SetActive(false);
    }

    public void InfoPanel()
    {
        InformationPanel.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        InformationPanel.SetActive(false);
    }

    public void ClosePanel()
    {
        environmentPanel.SetActive(false);
        PlayButton.SetActive(true);
    }

    public void GotoHome()
    {
        SceneManager.LoadScene("MainManu");
    }

    public void GotoGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void startGameInDayScene()
    {
        SceneManager.LoadScene("DayScene");
        environmentPanel.SetActive(false);
        CarController.GameOvelInDifferentScene = 0;
    }

    public void startGameInNightScene()
    {
        SceneManager.LoadScene("NightScene");
        environmentPanel.SetActive(false);
        CarController.GameOvelInDifferentScene = 1;

    }
}
