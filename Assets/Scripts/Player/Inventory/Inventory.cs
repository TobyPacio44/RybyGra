using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public InventoryUI ui;

    public Money money;
    public GameObject fishingRod;

    public int itemsCapacity;
    public List<ItemObject> items = new List<ItemObject>();
    public List<GameObject> unlockedItemsSlots = new List<GameObject>();
    public int fishesCapacity;
    public List<FishObject> fishes = new List<FishObject>();
    public List<GameObject> unlockedFishesSlots = new List<GameObject>();
    public List<ItemObject> zanêty = new List<ItemObject>();
    public List<ItemObject> przynêty = new List<ItemObject>();

    public EquipmentObject kij;
    public EquipmentObject kolowrotek;
    public EquipmentObject zylka;
    public EquipmentObject splawik;
    public EquipmentObject haczyk;

    public bool opened;
    private void Start()
    {
        SetUpEQ();
        money.UpdateMoney();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!opened) {
                player.GetComponent<CharacterController>().enabled = false;
                player.Screen.move = false;
                ui.gameObject.SetActive(true);
                UpdateEq();
                opened = !opened;
            }
            else
            {
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                ui.gameObject.SetActive(false);
                opened = !opened;
            }

        }
    }
    public void afterShop()
    {
        SetUpEQ();
        UpdateEq();
    }
    public void SetUpEQ()
    {
        foreach (GameObject x in ui.fishesItems)
        {
            x.SetActive(false);
        }
        for (int i = 0; i < fishesCapacity; i++)
        {
            //ui.fishesItems[i].SetActive(true);
            unlockedFishesSlots[i].SetActive(true);
        }
        foreach (GameObject x in ui.itemsItems)
        {
            x.SetActive(false);
        }
        for (int i = 0; i < itemsCapacity; i++)
        {
            //ui.itemsItems[i].SetActive(true);
            unlockedItemsSlots[i].SetActive(true);
        }
    }
    public void UpdateEq()
    {      
        foreach(GameObject x in ui.fishesItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.SetActive(false);
        }
        for (int i  = 0; i < fishes.Count; i++)
        {
            var element = ui.fishesItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = fishes[i].sprite;
        }

        foreach (GameObject x in ui.itemsItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.SetActive(false);
        }
        for (int i = 0; i < items.Count; i++)
        {
            var element = ui.itemsItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = items[i].sprite;
        }
    }

    public void ClickItemSlot(int slot)
    {
        var Object = items[slot - 1];

        if(Object.GetType()==typeof(EquipmentObject)) {
            Debug.Log("ok");
        }

    }
}
