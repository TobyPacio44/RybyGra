using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject building;
    public GameObject indicator;
    public int price;

    public void build(GameObject destroy)
    {
        int money = PlayerPrefs.GetInt("money");
        if (price <= money)
        {
            money -= price;
            PlayerPrefs.SetInt("money", money);
            building.SetActive(true);
            Destroy(indicator);
            Destroy(destroy);
        }
        else
        {
            return;
        }
    }
}
