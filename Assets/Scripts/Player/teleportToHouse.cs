using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportToHouse : MonoBehaviour
{
    public GameObject pos;

    private void Start()
    {
        Player.instance.gameObject.transform.position = pos.transform.position;
    }
}
