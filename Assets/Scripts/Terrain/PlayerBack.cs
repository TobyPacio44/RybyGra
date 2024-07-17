using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{
    public Transform point;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.instance.gameObject.transform.position = point.position;
        }
    }
}
