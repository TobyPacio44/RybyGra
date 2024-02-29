using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vision : MonoBehaviour
{
    public GameObject reticle;
    public GameObject hookedSquare;

    float MouseX;
    float MouseY;
    public float sensitivity;
    private void Update()
    {
        CameraMovement();
    }

    public void CameraMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        MouseX += Input.GetAxis("Mouse X") * sensitivity;
        MouseY += Input.GetAxis("Mouse Y") * sensitivity;
        MouseY = Mathf.Clamp(MouseY, -90, 90);

        transform.rotation = Quaternion.Euler(-MouseY, MouseX, 0);
    }
}