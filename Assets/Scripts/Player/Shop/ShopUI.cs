using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Player player;
    public GameObject buttonPrefab;
    public List<GameObject> slots;
    
    public void CreateList(List<ItemObject> items)
    {
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
    public void BuyItem(ItemObject item)
    {
        player.inventory.items.Add(item);
        player.inventory.afterShop();
    }

    public void Close(GameObject x)
    {
        x.SetActive(false);
    }
}
