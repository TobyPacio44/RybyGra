using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int hour;
    public int seconds;

    [SerializeField]
    private int secondsInAnHour;
    private float timer;
    private void Awake()
    {
        Instance = this;
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if(seconds > secondsInAnHour-1) { hour++; timer = 0; }
        if(hour > 23) { hour = 0; }
    }
}
