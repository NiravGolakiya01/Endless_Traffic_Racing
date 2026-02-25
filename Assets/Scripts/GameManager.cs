using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cars; // Array of car GameObjects or prefabs

    private void Start()
    {
        // Get the selected car index from PlayerPrefs
        int selectedCarIndex = PlayerPrefs.GetInt("selectedCar", 0);

        // Activate the selected car and deactivate the others
        //for (int i = 0; i < cars.Length; i++)
        //{
        //    cars[i].SetActive(i == selectedCarIndex);
        //}
    }

}
