using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLessCity : MonoBehaviour
{
    [SerializeField] Transform PlayerCarTransform;
    [SerializeField] Transform OtherCityTransform;
    [SerializeField] float halfLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCarTransform.position.z > transform.position.z + halfLength + 170f)
        {
            transform.position = new Vector3(0, 0, OtherCityTransform.position.z + halfLength * 2);
        }
    }
}