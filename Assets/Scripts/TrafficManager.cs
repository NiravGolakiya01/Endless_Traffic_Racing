using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[] Lanes;
    [SerializeField] GameObject[] TrafficVehicle;
    [SerializeField] CarController carControllers;
    [SerializeField] float minSpawnTime = 30f;
    [SerializeField] float maxSpawnTime = 60f;
    private float dynamicTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }

    IEnumerator TrafficSpawner()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            
            if(carControllers.CarSpeed() > 20f)
            {
                dynamicTimer = Random.Range(minSpawnTime, maxSpawnTime) / carControllers.CarSpeed();
                SpawnTrafficVehicle();  
            }
            yield return new WaitForSeconds(dynamicTimer);
        }
    }

    void SpawnTrafficVehicle()
    {
        int randomLanesIndex = Random.Range(0, Lanes.Length);
        int randomTrafficVehicleIndex = Random.Range(0, TrafficVehicle.Length);
        Instantiate(TrafficVehicle[randomTrafficVehicleIndex], Lanes[randomLanesIndex].position, Quaternion.identity);
    }
}
