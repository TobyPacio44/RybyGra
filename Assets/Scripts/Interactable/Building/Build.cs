using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Build : MonoBehaviour
{
    public GameObject building;
    public GameObject indicator;
    public int price;

    public void build()
    {
        int money = PlayerPrefs.GetInt("money");
        if (price <= money)
        {
            money -= price;
            PlayerPrefs.SetInt("money", money);
            Destroy(indicator);
            building.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
