using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] int currentCarIndex = 0;
    [SerializeField] GameObject[] CarModels;

    // Start is called before the first frame update
    void Start()
    {
        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        foreach(GameObject car in CarModels)
        {
            car.SetActive(false);
        }
        CarModels[currentCarIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNext()
    {
        CarModels[currentCarIndex].SetActive(false);

        currentCarIndex++;
        if(currentCarIndex == CarModels.Length)
        {
            currentCarIndex = 0;
        }

        CarModels[currentCarIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void ChangePrevious()
    {
        CarModels[currentCarIndex].SetActive(false);

        currentCarIndex--;
        if (currentCarIndex == -1)
        {
            currentCarIndex = CarModels.Length -1;
        }

        CarModels[currentCarIndex].SetActive(true);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }
}
