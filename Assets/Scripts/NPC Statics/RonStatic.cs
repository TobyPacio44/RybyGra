using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RonStatic : MonoBehaviour
{
    public static RonStatic instance;
    private void Awake()
    {
        instance = this;
    }
}
