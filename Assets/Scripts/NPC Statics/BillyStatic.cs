using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyStatic : MonoBehaviour
{
    public static BillyStatic instance;
    private void Awake()
    {
        instance = this;
    }
}
