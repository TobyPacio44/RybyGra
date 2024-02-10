using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatThrowing : MonoBehaviour
{
    public Rigidbody rb;
    public float force;
    public void Throw(Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
