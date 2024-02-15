using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public Transform cam;
    private void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
