using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] WheelCollider FrontRightWheelCollider;
    [SerializeField] WheelCollider BackRightWheelCollider;
    [SerializeField] WheelCollider FrontLeftWheelCollider;
    [SerializeField] WheelCollider BackLeftWheelCollider;

    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform backRightWheelTransform;
    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform backLeftWheelTransform;

    public Transform CarCentreOfMassTransform;

    public float motorForce = 100f;
    public float steeringAngle = 30f;
    public float BrakesForce = 1000f;

    private Rigidbody rb;
    private float VerticalInput;
    private float HorizontalInput;

    public static int GameOvelInDifferentScene
    {
        get { return PlayerPrefs.GetInt("GameOvelInDifferentScene", 0); }
        set { PlayerPrefs.SetInt("GameOvelInDifferentScene", value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CarCentreOfMassTransform.localPosition;


    }

    // Update is called once per frame

    void FixedUpdate()
    {
        MotorForce();
        UpdateWheel();
        GetInput();
        Steering();
        ApplyBrakes();
        PowerSteering();
        CarSpeed();
    }

    void GetInput()
    {
        VerticalInput = Input.GetAxis("Vertical");
        HorizontalInput = Input.GetAxis("Horizontal");
    }

    void ApplyBrakes()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FrontRightWheelCollider.brakeTorque = BrakesForce;
            BackRightWheelCollider.brakeTorque = BrakesForce;
            FrontLeftWheelCollider.brakeTorque = BrakesForce;
            BackLeftWheelCollider.brakeTorque = BrakesForce;
            rb.drag = 1f;
        }
        else
        {
            FrontRightWheelCollider.brakeTorque = 0f;
            BackRightWheelCollider.brakeTorque = 0f;
            FrontLeftWheelCollider.brakeTorque = 0f;
            BackLeftWheelCollider.brakeTorque = 0f;
            rb.drag = 0f;
        }
    }

    void MotorForce()
    {
        FrontRightWheelCollider.motorTorque = motorForce * VerticalInput;
        FrontLeftWheelCollider.motorTorque = motorForce * VerticalInput;
    }

    void Steering()
    {
        FrontRightWheelCollider.steerAngle = steeringAngle * HorizontalInput;
        FrontLeftWheelCollider.steerAngle = steeringAngle * HorizontalInput;
    }

    void PowerSteering()
    {
        // Smooth steering damping factor
        float steeringDamping = 5f; // Adjust for smoothness during path changes
        float rotationSpeed = 0.5f; // Additional speed control for path changes

        // Target rotation based on input
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        if (HorizontalInput != 0)
        {
            // Adjust target rotation based on HorizontalInput for smoother path changes
            float targetAngle = HorizontalInput * 20f; // Example: max rotation of 30 degrees based on input
            targetRotation = Quaternion.Euler(0, targetAngle, 0);
        }

        // Smoothly interpolate rotation towards the target rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            (steeringDamping + Mathf.Abs(HorizontalInput) * rotationSpeed) * Time.deltaTime
        );

        // Optional: If the rotation gets very close to the target, snap to it to prevent jitter
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation;
        }
    }

    void UpdateWheel()
    {
        RotateWheel(FrontRightWheelCollider, frontRightWheelTransform);
        RotateWheel(BackRightWheelCollider, backRightWheelTransform);
        RotateWheel(FrontLeftWheelCollider, frontLeftWheelTransform);
        RotateWheel(BackLeftWheelCollider, backLeftWheelTransform);
    }

    void RotateWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }

    public float CarSpeed()
    {
        float speed = rb.velocity.magnitude * 2.23693629f;
        return speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OtherCars")
        {
            if (GameOvelInDifferentScene == 0)
            {
                UIManager.Instance.GameOver();
            }
            else if (GameOvelInDifferentScene == 1)
            {
                UIManagerNight.Instance.GameOver();
            }      
        }
    }
}