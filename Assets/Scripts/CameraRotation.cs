using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float Speed = -10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Speed *  Time.deltaTime, 0);
    }
}
