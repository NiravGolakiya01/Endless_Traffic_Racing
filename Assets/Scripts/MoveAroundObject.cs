using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField] float _mouseSensitivity = 3.0f;

    private float _rotationX;
    private float _rotationY;

    [SerializeField] Transform _target;

    [SerializeField] float _distanceFromTarget = 6f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField] float _smoothTime = 0.2f;

    // Update is called once per frame
    void Update()
    {
        // Check if the right mouse button is held down
        if (Input.GetMouseButton(0)) // 0 corresponds to the right mouse button
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            _rotationX += mouseX;
            _rotationY -= mouseY; // Invert the Y axis for typical camera behavior

            _rotationY = Mathf.Clamp(_rotationY, 10, 25);

            Vector3 nextRotation = new Vector3(_rotationY, _rotationX, 0);
            _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
            transform.localEulerAngles = _currentRotation;

            transform.position = _target.position - transform.forward * _distanceFromTarget;
        }
    }
}
