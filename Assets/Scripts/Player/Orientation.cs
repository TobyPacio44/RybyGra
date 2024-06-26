using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public Transform player;
    public Vision cam;
    public float MouseX;

    private void Update()
    {
        if (cam.move)
        {
            MouseX += Input.GetAxis("Mouse X") * cam.sensitivity;

            player.transform.rotation = Quaternion.Euler(0, MouseX, 0);
        }
    }
}
