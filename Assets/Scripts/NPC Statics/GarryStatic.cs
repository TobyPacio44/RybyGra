using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarryStatic : MonoBehaviour
{
    public static GarryStatic instance;
    private void Awake()
    {
        instance = this;
    }
}
