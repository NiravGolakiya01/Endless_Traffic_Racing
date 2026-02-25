using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerNight : MonoBehaviour
{
    public static UIManagerNight Instance;

    [SerializeField] TextMeshProUGUI SpeedText;
    [SerializeField] TextMeshProUGUI DistanceText;

    [SerializeField] CarController carController;
    [SerializeField] Transform carTransform;

    [Header("GameOverScore")]
    [SerializeField] TextMeshProUGUI lastScoreText;
    [SerializeField] TextMeshProUGUI yourHighScoreText;
    [SerializeField] TextMeshProUGUI lastDistanceText;
    [SerializeField] TextMeshProUGUI yourHighDistanceText;
    [SerializeField] TextMeshProUGUI moneyEarnedText; // Money earned in this session
    [SerializeField] TextMeshProUGUI totalMoneyText;  // Total money the player has
    [SerializeField] GameObject newHightScoreImage;
    [SerializeField] GameObject gameOverPanel;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI bestText;

    private float speed = 0f;
    private float distance = 0f;
    private int score = 0;
    private int bestScore;
    private float bestDistance;
    private int milestoneMoney = 0; // Money earned in the current session
    private int totalMoney = 0; // Total money accumulated across games
    private bool countScore;

    private int milestone500m = 0; // To track how many 500m milestones have been reached

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Load the best score, best distance, and total money from PlayerPrefs
        bestScore = PlayerPrefs.GetInt("nightBestScore", 0);
        bestDistance = PlayerPrefs.GetFloat("nightBestDistance", 0f);
        totalMoney = PlayerPrefs.GetInt("totalMoney", 0);

        bestText.text = bestScore.ToString();
        totalMoneyText.text = totalMoney.ToString();
        gameOverPanel.SetActive(false); // Ensure the Game Over panel is hidden at start
        GameStart();
    }

    void Update()
    {
        UpdateDistanceUI();
        UpdateSpeedUI();
        CheckMilestones(); // Check if the player has reached money milestones
    }

    void UpdateDistanceUI()
    {
        distance = carTransform.position.z / 1000;
        DistanceText.text = distance.ToString("0.00") + " Km";
    }

    void UpdateSpeedUI()
    {
        speed = carController.CarSpeed();
        SpeedText.text = speed.ToString("0") + " Km/h";
    }

    public void GameStart()
    {
        countScore = true;
        StartCoroutine(UpdateScore());
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        // Update the last score, distance, and money earned in the Game Over panel
        lastScoreText.text = score.ToString();
        lastDistanceText.text = distance.ToString("0.00") + " Km";
        moneyEarnedText.text = milestoneMoney.ToString();

        // Update the high score if the current score exceeds it
        if (score >= bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("nightBestScore", bestScore);
            newHightScoreImage.SetActive(true);
        }
        yourHighScoreText.text = bestScore.ToString();

        // Update the high distance if the current distance exceeds it
        if (distance > bestDistance)
        {
            bestDistance = distance;
            PlayerPrefs.SetFloat("nightBestDistance", bestDistance);
        }
        yourHighDistanceText.text = bestDistance.ToString("0.00") + " Km";

        // Add earned money to total money and save it
        totalMoney += milestoneMoney;
        PlayerPrefs.SetInt("totalMoney", totalMoney);

        totalMoneyText.text = totalMoney.ToString();

        countScore = false; // Stop the score counter
    }

    IEnumerator UpdateScore()
    {
        while (countScore)
        {
            yield return new WaitForSeconds(1f);
            score++;
            ScoreText.text = score.ToString(); // Update the score in real-time

            // Update the best score in real-time if it matches or exceeds
            if (score >= bestScore)
            {
                bestScore = score;
                bestText.text = bestScore.ToString(); // Update the best score display
            }
        }
    }

    void CheckMilestones()
    {
        // Check if the player has covered another 500m
        if (distance >= (0.5f * (milestone500m + 1)))
        {
            milestone500m++;
            milestoneMoney += 10; // Add $10 for every 500m milestone
        }
    }

    public void RestartGame()
    {
        // Reload the scene and reset the time scale
        SceneManager.LoadScene("NightScene");
        Time.timeScale = 1f;
    }

    public void GotoHome()
    {
        SceneManager.LoadScene("MainManu");
        Time.timeScale = 1f;
    }
}
