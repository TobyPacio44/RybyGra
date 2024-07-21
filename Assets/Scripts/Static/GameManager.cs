using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Material nightSkyBox;
    public Material daySkyBox;
    public GameObject day;
    public GameObject dayPref;

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

        if(hour == 5 && day == null)
        {
            StartDay();
        }
    }

    public void StartDay()
    {
        hour = 5;
        RenderSettings.skybox = daySkyBox;
        RenderSettings.fog = true;
        NPCS.Instance.gameObject.SetActive(true);
        NPCS.Instance.shop.gameObject.SetActive(true);
        NPCS.Instance.skup.gameObject.SetActive(true);
        day = Instantiate(dayPref);
    }
    public void StartNight()
    {
        RenderSettings.skybox = nightSkyBox;
        RenderSettings.fog = false;
        Destroy(day);
        day = null;
        NPCS.Instance.gameObject.SetActive(false);
        NPCS.Instance.shop.gameObject.SetActive(false);
        NPCS.Instance.skup.gameObject.SetActive(false);
    }
}
