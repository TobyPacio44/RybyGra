using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject credits;

    public GameObject sfx;
    public GameObject music;
    private void Start()
    {
        float z = sfx.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("sfxVolume", z);

        float y = music.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("musicVolume", y);
    }

    public void openMain()
    {
        main.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);
    }
    public void openOptions()
    {
        main.SetActive(false);
        options.SetActive(true);
        credits.SetActive(false);
    }
    public void openCredits()
    {
        main.SetActive(false);
        options.SetActive(false);
        credits.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenURL(string x)
    {
        Application.OpenURL(x);
    }
}
