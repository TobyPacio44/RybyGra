using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public Transform player;
    public Vision cam;
    public float MouseX;
    public GameObject pointer;
    private void Update()
    {
        if (cam.move)
        {
            MouseX += Input.GetAxis("Mouse X") * cam.sensitivity;

            player.transform.rotation = Quaternion.Euler(0, MouseX, 0);
        }
    }

    public void LookAt(GameObject lookAt)
    {
        pointer.transform.LookAt(lookAt.transform.position);
        MouseX = pointer.transform.rotation.eulerAngles.y;
    }
}
