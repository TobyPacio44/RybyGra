using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Shop : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        SellFish(player);
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
