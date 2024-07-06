using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject credits;

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
}
