using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandyStatic : MonoBehaviour
{
    public static RandyStatic instance;
    private void Awake()
    {
        instance = this;
    }
}
