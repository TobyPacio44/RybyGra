using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCS : MonoBehaviour
{
    public static NPCS Instance;
    public GameObject shop;
    public GameObject skup;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
