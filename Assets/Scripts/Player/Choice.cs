using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public Shop shop;
    public void Buy()
    {
        Debug.Log("Buy");
        shop.Buy();
        gameObject.SetActive(false);
    }

    public void Sell()
    {
        Debug.Log("Sell");
        shop.Sell();
        gameObject.SetActive(false);
    }
}
