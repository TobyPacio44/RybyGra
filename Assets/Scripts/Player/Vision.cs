using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vision : MonoBehaviour
{
    public static Vision instance;

    public bool move = true;
    public GameObject reticle;
    public GameObject hookedSquare;
    public Orientation orientation;

    //public float MouseX;
    public float MouseY;
    public float sensitivity;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        if (move)
        {
            CameraMovement();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void CameraMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Biore mouseX od orientacji jednak
        //MouseX += Input.GetAxis("Mouse X") * sensitivity;
        MouseY += Input.GetAxis("Mouse Y") * sensitivity;
        MouseY = Mathf.Clamp(MouseY, -90, 90);

        transform.rotation = Quaternion.Euler(-MouseY, orientation.MouseX, 0);
    }
}
