using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float controllerSensitivity = 100.0f;
    public Transform playerBody;
    public Joystick rightJoystick;

    private float XRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal;
        float vertical;

        if (Application.platform != RuntimePlatform.Android)
        {
            horizontal = Input.GetAxis("Mouse X") * controllerSensitivity;
            vertical = Input.GetAxis("Mouse Y") * controllerSensitivity;
        }
        else
        {
            horizontal = rightJoystick.Horizontal * controllerSensitivity;
            vertical = rightJoystick.Vertical * controllerSensitivity;
        }

        XRotation -= vertical;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * horizontal);
    }
}
