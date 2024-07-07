using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject iconsDropDown;
    public GameObject nickInput;

    public void OpenIcons()
    {
        if (iconsDropDown.activeSelf)
        {
            iconsDropDown.SetActive(false);
        }
        else
        {
            iconsDropDown.SetActive(true);
        }
    }

    private void Update()
    {
        string nick = nickInput.GetComponent<TextMeshProUGUI>().text;
        PlayerPrefs.SetString("Name", nick);
    }

    public void changeSfx(GameObject x)
    {
        float z = x.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("sfxVolume", z);
    }
    public void changeMusic(GameObject x)
    {
        float z = x.GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("musicVolume", z);
    }
}
