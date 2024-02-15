using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public Transform player;
    public Vision cam;
    float MouseX;

    private void Update()
    {
        MouseX += Input.GetAxis("Mouse X") * cam.sensitivity;

        player.transform.rotation = Quaternion.Euler(0, MouseX, 0);
    }
}
