using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public Shop shop;
    public void Buy()
    {
        shop.Buy();
        gameObject.SetActive(false);
    }

    public void Sell()
    {
        shop.Sell();
        gameObject.SetActive(false);
    }
}
