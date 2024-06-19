using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;


public class Inventory : MonoBehaviour
{
    public Player player;
    public InventoryUI ui;
    public Money money;
    public FishingRod fishingRod;

    public List<InventoryItem> items = new List<InventoryItem>();
    //public List<ItemObject> items = new List<ItemObject>();
    public List<GameObject> unlockedItemsSlots = new List<GameObject>();

    public List<FishObject> fishes = new List<FishObject>();
    public List<GameObject> unlockedFishesSlots = new List<GameObject>();

    public List<ItemObject> zanêty = new List<ItemObject>();
    public List<InventoryItem> bait = new List<InventoryItem>();
    public List<GameObject> unlockedBaitSlots = new List<GameObject>();

    public EquipmentObject kij;
    public EquipmentObject kolowrotek;
    public EquipmentObject zylka;
    public EquipmentObject splawik;
    public EquipmentObject haczyk;

    public int itemsCapacity;
    public int fishesCapacity;
    private int previous_bait;
    private bool opened;
    private void Start()
    {
        previous_bait = 0;
        afterShop();
        money.UpdateMoney();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(player.buffManager.ui.activeSelf == true)
            {
                player.buffManager.ui.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }
            if (!opened)
            {
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
        UpdateEquipment();
        CalculateRodPower();
        InstantiateBait();

        fishingRod.canFish = canFish();
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
        foreach (GameObject x in ui.przynetyItems)
        {
            x.SetActive(false);
        }
    }
    public void UpdateEq()
    {
        foreach (GameObject x in ui.fishesItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.SetActive(false);
        }
        for (int i = 0; i < fishes.Count; i++)
        {
            var element = ui.fishesItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = fishes[i].sprite;
        }

        foreach (GameObject x in ui.itemsItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.SetActive(false);
            x.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 0.ToString();
        }
        for (int i = 0; i < items.Count; i++)
        {
            var element = ui.itemsItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = items[i].item.sprite;

            if (items[i].amount > 0)
            {
                element.transform.GetChild(0).gameObject.SetActive(true);
                element.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = items[i].amount.ToString();
            }
            else { element.transform.GetChild(0).gameObject.SetActive(false); }
        }
    }
    public void UpdateEquipment()
    {
        if (kij != null) {          ui.fishRodItems[0].GetComponent<UnityEngine.UI.Image>().sprite = kij.sprite; }
        if (kolowrotek != null) {   ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = kolowrotek.sprite; }
        if (zylka != null) {        ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = zylka.sprite; }
        if (haczyk != null) {       ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = haczyk.sprite; }
        if (splawik != null) {      ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = splawik.sprite; }

        for(int i = 0; i < 3; i++)
        {
            if (bait[i].item != null)
            {
                ui.przynetyItems[i].SetActive(true);
                ui.przynetyItems[i].GetComponent<UnityEngine.UI.Image>().sprite = bait[i].item.sprite;
                ui.przynetyItems[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bait[i].amount.ToString();
            }
            else
            {
                ui.przynetyItems[i].SetActive(false);
                //ui.przynetyItems[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 0.ToString();
            }
        }

    }
    public void InstantiateBait()
    {
        foreach(Transform x in fishingRod.components.bait.transform) { Destroy(x.gameObject); }
        foreach(InventoryItem x in bait)
        {
            if (x.item != null)
            {
                Instantiate(x.item.prefab, fishingRod.components.bait.transform);
            }
        }
    }
    public void InstantiateRod(GameObject Parent, EquipmentObject eq)
    {
        if (kij == null)
        {
            fishingRod.gameObject.SetActive(false);
        }
        else { fishingRod.gameObject.SetActive(true); }

        foreach (Transform child in Parent.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(eq.prefab, Parent.transform);
    }

    public void ClickItemSlot(int slot)
    {
        var Object = items[slot - 1];
        if (Object.item is EquipmentObject)
        {
            var eq = (EquipmentObject)Object.item;
            var amount = Object.amount;

            if (eq.eqType == EquipmentObject.EquipmentType.Kij)
            {
                if (kij == null) { kij = eq; items.Remove(Object); }
                else
                {
                    var ram = kij;
                    kij = eq;
                    items[slot - 1].item = ram;
                }

                InstantiateRod(fishingRod.components.kij, eq);
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Kolowrotek)
            {
                if (kolowrotek == null) { kolowrotek = eq; items.Remove(Object); }
                else
                {
                    var ram = kolowrotek;
                    kolowrotek = eq;
                    items[slot - 1].item = ram;
                }
                InstantiateRod(fishingRod.components.kolowrotek, eq);
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Zylka)
            {
                if (zylka == null) { zylka = eq; items.Remove(Object); }
                else
                {
                    var ram = zylka;
                    zylka = eq;
                    items[slot - 1].item = ram;
                }
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Haczyk)
            {
                if (haczyk == null) { haczyk = eq; items.Remove(Object); }
                else
                {
                    var ram = haczyk;
                    haczyk = eq;
                    items[slot - 1].item = ram;
                }

                HandleBaitSlotManagement(eq);
                InstantiateRod(fishingRod.components.haczyk, eq);
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Splawik)
            {
                if (splawik == null) { splawik = eq; items.Remove(Object); }
                else
                {
                    var ram = splawik;
                    splawik = eq;
                    items[slot - 1].item = ram;
                }
            }

            if (eq.eqType == EquipmentObject.EquipmentType.Przyneta)
            {
                if (!unlockedBaitSlots[0].activeSelf) { return; }
                if (previous_bait > 2) { previous_bait = 0; }
                if (!unlockedBaitSlots[2].activeSelf && previous_bait > 1){previous_bait = 0;}
                if (!unlockedBaitSlots[1].activeSelf && previous_bait > 0){previous_bait = 0;}

                if(bait[previous_bait].item == eq) { bait[previous_bait].amount += amount; items.Remove(Object); }
                else if (bait[previous_bait].item == null) 
                { 
                    bait[previous_bait].item = eq; 
                    bait[previous_bait].amount = amount; 
                    items.Remove(Object); 
                    previous_bait++; 
                }
                else
                {
                    var ram = bait[previous_bait].item;
                    var ram2 = bait[previous_bait].amount;

                    bait[previous_bait].item = eq;
                    bait[previous_bait].amount = amount;

                    items[slot - 1].item = ram;
                    items[slot - 1].amount = ram2;
                    previous_bait++;
                }
            }
        }

        afterShop();
    }
    public void HandleBaitSlotManagement(EquipmentObject haczyk)
    {
        previous_bait = 0;
        foreach(GameObject x in unlockedBaitSlots)
        {
            x.SetActive(false);
        }

        if(haczyk.power > -1) { unlockedBaitSlots[0].SetActive(true); }
        if(haczyk.power > 25) { unlockedBaitSlots[1].SetActive(true); }
        if(haczyk.power > 625) { unlockedBaitSlots[2].SetActive(true); }

        for (int i = 0; i < 3; i++)
        {
            if (!unlockedBaitSlots[i].activeSelf && bait[i].item != null)
            {
                AddToInventory(bait[i].item, 1);
                bait[i] = null;
                ui.przynetyItems[i].GetComponent<UnityEngine.UI.Image>().sprite = null;
            }
        }
    }
    public bool canFish()
    {
        if (kij == null) { return false; }
        if (kolowrotek == null) { return false; }
        if (zylka == null) { return false; }
        if (haczyk == null) { return false; }
        if (splawik == null) { return false; }
        return true;
    }
    public void CalculateRodPower()
    {
        int a = 0;
        if (kij != null) { a += kij.power; }
        if (kolowrotek != null) { a += kolowrotek.power; }
        if (zylka != null) { a += zylka.power; }
        if (haczyk != null) { a += haczyk.power; }
        if (splawik != null) { a += splawik.power; }

        fishingRod.stats.RodPower = a;
        ui.rodPower.text = fishingRod.stats.RodPower.ToString();
    }

    public void AddToInventory(ItemObject item, int amount)
    {
        foreach (InventoryItem x in items)
        {
            if(x.item == item)
            {
                x.amount += amount;
                return;
            }
        }
        items.Add(new InventoryItem(item, amount));
    }

    public void TakeOneBait()
    {
        for(int i = bait.Count-1;  i >= 0; i--)
        {
            if (bait[i].item != null)
            {
                bait[i].amount -= 1;
                if (bait[i].amount < 1) { bait[i].item = null; bait[i].amount = 0; }
            }
        }
        afterShop();
    }
}
[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;

    public InventoryItem(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}