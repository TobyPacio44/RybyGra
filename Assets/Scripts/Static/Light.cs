using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public GameManager gameManager;
    public void Awake()
    {
        gameManager = GameManager.Instance;
    }
    public void SetNight()
    {
        gameManager.StartNight();
    }
}
