using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    private Transform PlayerCarTransform;
    private Transform CameraPointTransform;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        CameraPointTransform = PlayerCarTransform.Find("CameraPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(PlayerCarTransform);
        transform.position = Vector3.SmoothDamp(transform.position, CameraPointTransform.position,ref velocity, 5f*Time.deltaTime);
    }
}
