using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform lookAt;
    private void Start()
    {
        lookAt = Vision.instance.transform;
    }
    private void Update()
    {
        transform.LookAt(lookAt);
    }
}
