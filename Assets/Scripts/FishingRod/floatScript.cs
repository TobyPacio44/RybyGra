using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public FishingRod rod;

    public Rigidbody rb;
    public floatAnimation floatAnimation;
    public float force;
    public void Throw(Vector3 direction)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FishList>() != null)
        {
            AudioManager.instance.PlaySFX("Water");
            rod.fishList = other.GetComponent<FishList>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FishList>() == null)
        {
            Debug.Log(1);
            rod.Hooked(false, null);
        }
    }
}
