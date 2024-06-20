using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Player player;
    public GameObject buttonPrefab;
    public List<GameObject> slots;

    public void ClearList()
    {
        foreach(GameObject x in slots)
        {
            foreach(Transform child in x.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    public void CreateList(List<ItemObject> items)
    {
        ClearList();

        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

        int i = 0;
        foreach (ItemObject item in items)
        {
            GameObject newButton = Instantiate(buttonPrefab, slots[i].transform) ;
            i++;
            newButton.GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
            foreach(Transform child in newButton.transform){
                if (child.name == "Sprite") { child.GetComponent<Image>().sprite = item.sprite; }
                if (child.name == "Cost") { child.GetComponent<TextMeshProUGUI>().text = item.price.ToString(); }
            }
        }
    }

    public void CreateSellList()
    {
        ClearList();

        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

        int i = 0;
        foreach (InventoryItem item in player.inventory.items)
        {
            GameObject newButton = Instantiate(buttonPrefab, slots[i].transform);
            i++;
            newButton.GetComponent<Button>().onClick.AddListener(() => SellItem(item));
            foreach (Transform child in newButton.transform)
            {
                if (child.name == "Sprite") { child.GetComponent<Image>().sprite = item.item.sprite; }
                int newPrice = item.item.price / 2;
                if (child.name == "Cost") { child.GetComponent<TextMeshProUGUI>().text = newPrice.ToString(); }
            }
        }
    }
    public void BuyItem(ItemObject item)
    {
        int money = PlayerPrefs.GetInt("money");
        if(item.price <= money)
        {
            money -= item.price;
            PlayerPrefs.SetInt("money", money);
            player.inventory.AddToInventory(item, 1);
            player.inventory.afterShop();
        }      
    }

    public void SellItem(InventoryItem item)
    {
        int money = PlayerPrefs.GetInt("money");
        int newPrice = item.item.price / 2;
        money += newPrice;
        PlayerPrefs.SetInt("money", money);
        if (item.amount > 0) { item.amount -= 1; }
        else
        {
            player.inventory.items.Remove(item);
        }
        player.inventory.afterShop();
        CreateSellList();
    }

    public void Close(GameObject x)
    {
        x.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        player.Screen.move = true;
    }
}
