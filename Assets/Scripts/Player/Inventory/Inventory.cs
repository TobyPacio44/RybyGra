
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

    public GameObject tier;

    public int itemsCapacity;
    public int fishesCapacity;
    private int previous_bait;
    public bool opened;
    private void Start()
    {
        previous_bait = 0;
        afterShop();
        money.UpdateMoney();
    }
    public void afterShop()
    {
        SetUpEQ();
        UpdateEq();
        UpdateEquipment();
        checkRod();
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
            else { ui.fishRodItems[0].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[0]; }

        if (kolowrotek != null) {   ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = kolowrotek.sprite; }
            else { ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[1]; }

        if (zylka != null) {        ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = zylka.sprite; }
            else { ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[2]; }

        if (haczyk != null) {       ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = haczyk.sprite; }
            else { ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[3]; }

        if (splawik != null) {      ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = splawik.sprite; }
            else { ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[4]; }


        for (int i = 0; i < 3; i++)
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
    public void checkRod()
    {
        if (kij == null)
        {
            fishingRod.gameObject.SetActive(false);
        }
        else { fishingRod.gameObject.SetActive(true); }
    }

    public void InstantiateRod(GameObject Parent, EquipmentObject eq)
    {
        foreach (Transform child in Parent.transform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(eq.prefab, Parent.transform);
    }

    public void unEquip(int i)
    {
        if (fishingRod.State != FishingRod.state.idle)
        {
            return;
        }

        switch (i)
        {
            case 0: if (kij != null){          AddToInventory(kij, 0); }            kij = null;
                foreach (Transform child in fishingRod.components.kij.transform)
                {
                    Destroy(child.gameObject);
                }
                break;
            case 1: if (kolowrotek != null){   AddToInventory(kolowrotek, 0); }     kolowrotek = null;
                foreach (Transform child in fishingRod.components.kolowrotek.transform)
                {
                    Destroy(child.gameObject);
                }
                break;


            case 2: if (zylka != null){        AddToInventory(zylka, 0); }          zylka = null;
                break;


            case 3: if (splawik != null){      AddToInventory(splawik, 0); }        splawik = null; 
                break;


            case 4: if (haczyk != null){       AddToInventory(haczyk, 0); }         haczyk = null;
                foreach (Transform child in fishingRod.components.haczyk.transform)
                {
                    Destroy(child.gameObject);
                }
                break;
            case 5:
                if (bait[0].item != null) { AddToInventory(bait[0].item, bait[0].amount); }
                bait[0].item = null;
                foreach (Transform child in fishingRod.components.bait.transform)
                {
                        Destroy(child.gameObject);
                }
                break;
            case 6:
                if (bait[1].item != null) { AddToInventory(bait[1].item, bait[1].amount); }
                bait[1].item = null;
                foreach (Transform child in fishingRod.components.bait.transform)
                {
                        Destroy(child.gameObject);
                }
                break;
            case 7:
                if (bait[2].item != null) { AddToInventory(bait[2].item, bait[2].amount); }
                bait[2].item = null;
                foreach (Transform child in fishingRod.components.bait.transform)
                {
                        Destroy(child.gameObject);
                }
                break;
        }

        afterShop();
    }
    public void ClickItemSlot(int slot)
    {
        if (fishingRod.State != FishingRod.state.idle)
        {
            return;
        }

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

        //tier.SetActive(false);
        if (a > -1)     { tier.SetActive(true); tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[0]; }
        if (a > 99)     {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[1]; }
        if (a > 999)    {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[2]; }
        if (a > 2499)   {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[3]; }
        if (a > 7499)   {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[4]; }
        if (a > 29999)  {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[5]; }

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