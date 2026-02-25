using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI totalMoneyText;
    [SerializeField] TextMeshProUGUI carPriceText;
    [SerializeField] public GameObject playButton;
    [SerializeField] public GameObject buyButton;
    [SerializeField] public GameObject startEPButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    public List<Sprite> Text = new List<Sprite>();
    public Material material;

    private int currentCar;
    private int totalMoney = 0;

    private int[] carPrices = { 0, 30, 60, 80, 100, 230 }; // Prices for cars
    private bool[] purchasedCars;

    private void Awake()
    {
        purchasedCars = new bool[transform.childCount]; // Initialize purchase array
        LoadPurchasedCars();
        SelectCar(0);
    }

    void Start()
    {
        totalMoney = PlayerPrefs.GetInt("totalMoney", 0);
        totalMoneyText.text = totalMoney.ToString();
    }

    private void Update()
    {
        PlayerPrefs.SetInt("totalMoney", totalMoney);
        totalMoneyText.text = totalMoney.ToString();
    }

    private void SelectCar(int _index)
    {
        currentCar = _index;

        // Enable/disable previous and next buttons
        previousButton.interactable = (_index != 0);
        nextButton.interactable = (_index != transform.childCount - 1);

        // Show only the selected car
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }

        // Update UI for car price, buy button, and play button
        if (purchasedCars[_index])
        {
            carPriceText.text = "Owned"; 
            buyButton.SetActive(false); 
            playButton.SetActive(true); 
            startEPButton.SetActive(true); 
        }
        else
        {
            carPriceText.text = carPrices[_index].ToString(); // Show car price
            buyButton.SetActive(totalMoney >= carPrices[_index]); // Enable if enough money
            playButton.SetActive(false); // Disable the Play button for unowned cars
            startEPButton.SetActive(false);
        }
    }

    public void ChangeCar(int _change)
    {
        currentCar += _change;
        material.mainTexture = Text[currentCar].texture;
        SelectCar(currentCar);
    }

    public void BuyCar()
    {
        if (totalMoney >= carPrices[currentCar] && !purchasedCars[currentCar])
        {
            // Deduct money and mark car as purchased
            totalMoney -= carPrices[currentCar];
            purchasedCars[currentCar] = true;

            // Save data
            PlayerPrefs.SetInt("totalMoney", totalMoney);
            SavePurchasedCars();

            // Refresh the UI
            SelectCar(currentCar);
        }
    }

    public void PlayGame()
    {
        material.mainTexture = Text[currentCar].texture;
        // Save the selected car index to PlayerPrefs
        PlayerPrefs.SetInt("selectedCar", currentCar);

        // Load the game scene (replace "DayScene" with your scene name)
        SceneManager.LoadScene("DayScene");
    }

    private void LoadPurchasedCars()
    {
        for (int i = 0; i < purchasedCars.Length; i++)
        {
            purchasedCars[i] = PlayerPrefs.GetInt("purchasedCar_" + i, 0) == 1;
        }
    }

    private void SavePurchasedCars()
    {
        for (int i = 0; i < purchasedCars.Length; i++)
        {
            PlayerPrefs.SetInt("purchasedCar_" + i, purchasedCars[i] ? 1 : 0);
        }
    }
}
