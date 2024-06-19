using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Shop : MonoBehaviour, IInteractable
{
    public List<ItemObject> sellable;

    public void Interact(Player player)
    {
        if (player.ShopUI.gameObject.active == false)
        {
            player.ShopUI.CreateList(sellable);
            player.ShopUI.gameObject.SetActive(true);
        }
    }

    public void SellFish(Player player)
    {
        int money = PlayerPrefs.GetInt("money");
        foreach (ItemObject fishes in player.inventory.fishes)
        {
            money += fishes.price;
        }
        player.inventory.afterShop();
        player.inventory.fishes.Clear();
        PlayerPrefs.SetInt("money", money);
    }
}
