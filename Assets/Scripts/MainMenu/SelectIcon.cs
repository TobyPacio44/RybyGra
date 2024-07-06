using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectIcon : MonoBehaviour
{
    public GameObject icon;
    public void SelectIconID(int i)
    {
        PlayerPrefs.SetInt("icon", i);
        icon.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("playerIcons/"+i);

    }
    public void back()
    {
        this.gameObject.SetActive(false);
    }
}
