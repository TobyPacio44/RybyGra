using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonStatic : MonoBehaviour
{
    public static DonStatic instance;
    private void Awake()
    {
        instance = this;
    }
}
