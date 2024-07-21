using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class Shop : MonoBehaviour, IInteractable
{
    public List<ItemObject> bait;
    public List<ItemObject> kije;
    public List<ItemObject> kolowrotki;
    public List<ItemObject> zylki;
    public List<ItemObject> haczyki;    
    public List<ItemObject> splawiki;
    public List<ItemObject> buffs;
    public List<ItemObject> nests;

    public Player localPlayer;

    public void Buy()
    {
            localPlayer.ShopUI.sections.gameObject.SetActive(true);
            localPlayer.ShopUI.CreateList(kije, false);
            localPlayer.ShopUI.gameObject.SetActive(true);
    }

    public void Sell()
    {
            localPlayer.ShopUI.sections.gameObject.SetActive(false);
            localPlayer.ShopUI.CreateSellList();
            localPlayer.ShopUI.gameObject.SetActive(true);
    }

    public void Interact(Player player)
    {
        if (player.choice.gameObject.active == false && player.skupUI.gameObject.active == false)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.Screen.move = false;

            localPlayer = player;
            player.choice.shop = this;
            player.choice.gameObject.SetActive(true);
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
