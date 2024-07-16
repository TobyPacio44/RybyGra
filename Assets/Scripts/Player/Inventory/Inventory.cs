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
    public static Inventory instance;

    public Player player;
    public InventoryUI ui;
    public Money money;
    public FishingRod fishingRod;

    public int itemsCapacity;
    public int fishesCapacity;
    public List<InventoryItem> items = new List<InventoryItem>();
    public List<FishObject> fishes = new List<FishObject>();
    public InventoryItem bait;
    public EquipmentObject kij;
    public EquipmentObject kolowrotek;
    public EquipmentObject zylka;
    public EquipmentObject splawik;
    public EquipmentObject haczyk;
    public EquipmentObject nest;

    //public List<ItemObject> zanêty = new List<ItemObject>();
    //public List<ItemObject> items = new List<ItemObject>();
    public List<GameObject> unlockedItemsSlots = new List<GameObject>();
    public List<GameObject> unlockedFishesSlots = new List<GameObject>();
    public GameObject unlockedBaitSlots;
    public GameObject tier;
    public GameObject icon;
    public GameObject nick;

    //private int previous_bait;
    public bool opened;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //previous_bait = 0;
        afterShop();
        money.UpdateMoney();
    }
    public void afterShop()
    {
        SetUpPlayer();
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
        for (int i = 0; i < 16; i++)
        {
            unlockedFishesSlots[i].SetActive(false);
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

        //ui.fishRodItems[5].SetActive(false);
    }
    public void UpdateEq()
    {
        foreach (GameObject x in ui.fishesItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.GetComponent<ItemHover>().item = null;
            x.SetActive(false);
        }
        for (int i = 0; i < fishes.Count; i++)
        {
            var element = ui.fishesItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = fishes[i].sprite;
            element.GetComponent<ItemHover>().item = fishes[i];
        }

        foreach (GameObject x in ui.itemsItems)
        {
            x.GetComponent<UnityEngine.UI.Image>().sprite = null;
            x.SetActive(false);
            x.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 0.ToString();
            x.GetComponent<ItemHover>().item = null;
        }
        for (int i = 0; i < items.Count; i++)
        {
            var element = ui.itemsItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = items[i].item.sprite;
            element.GetComponent<ItemHover>().item = items[i].item;

            if (items[i].amount > 0)
            {
                element.transform.GetChild(0).gameObject.SetActive(true);
                element.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = items[i].amount.ToString();
            }
            else { element.transform.GetChild(0).gameObject.SetActive(false); }
        }
    }

    public void SetUpPlayer()
    {
        icon.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("playerIcons/" + PlayerPrefs.GetInt("icon"));
        nick.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name");
    }
    public void UpdateEquipment()
    {
        if (kij != null) {          ui.fishRodItems[0].GetComponent<UnityEngine.UI.Image>().sprite = kij.sprite;
            ui.fishRodItems[0].GetComponent<ItemHover>().item = kij; }
            else { ui.fishRodItems[0].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[0];
            ui.fishRodItems[0].GetComponent<ItemHover>().item = null;
        }

        if (kolowrotek != null) {   ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = kolowrotek.sprite;
            ui.fishRodItems[1].GetComponent<ItemHover>().item = kolowrotek;
        }
            else { ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[1];
            ui.fishRodItems[1].GetComponent<ItemHover>().item = null;
        }

        if (zylka != null) {        ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = zylka.sprite;
            ui.fishRodItems[2].GetComponent<ItemHover>().item = zylka;
        }
            else { ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[2];
            ui.fishRodItems[2].GetComponent<ItemHover>().item = null;
        }


        if (haczyk != null) {       ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = haczyk.sprite; 
                                    unlockedBaitSlots.SetActive(true);
            ui.fishRodItems[3].GetComponent<ItemHover>().item = haczyk;
        }
            else { ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[3];
            unlockedBaitSlots.SetActive(false);
            ui.fishRodItems[3].GetComponent<ItemHover>().item = null;
        }

        if (splawik != null) {      ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = splawik.sprite;
            ui.fishRodItems[4].GetComponent<ItemHover>().item = splawik;
        }
            else { ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[4];
            ui.fishRodItems[4].GetComponent<ItemHover>().item = null;
        }

            if (bait.item != null)
            {
                ui.fishRodItems[5].SetActive(true);
                ui.fishRodItems[5].GetComponent<UnityEngine.UI.Image>().sprite = bait.item.sprite;
                ui.fishRodItems[5].transform.GetChild(0).gameObject.SetActive(true);
                ui.fishRodItems[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = bait.amount.ToString();
                ui.fishRodItems[5].GetComponent<ItemHover>().item = bait.item;
            }
            else
            {
                ui.fishRodItems[5].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[5];
                ui.fishRodItems[5].GetComponent<ItemHover>().item = null;
                ui.fishRodItems[5].transform.GetChild(0).gameObject.SetActive(false);
            }

        if (nest != null) { ui.fishRodItems[6].GetComponent<UnityEngine.UI.Image>().sprite = nest.sprite;
            ui.fishRodItems[6].GetComponent<ItemHover>().item = nest;
        }
        else { ui.fishRodItems[6].GetComponent<UnityEngine.UI.Image>().sprite = ui.fishRodItemsPlaceHolders[6];
            ui.fishRodItems[6].GetComponent<ItemHover>().item = null;
        }
    }
    public void InstantiateBait()
    {
        foreach(Transform x in fishingRod.components.bait.transform) { Destroy(x.gameObject); }
        //foreach(InventoryItem x in bait)
        //{
        //   if (x.item != null)
        //    {
        //        Instantiate(x.item.prefab, fishingRod.components.bait.transform);
        //    }
        //}
        if (bait.item != null)
        {
            Instantiate(bait.item.prefab, fishingRod.components.bait.transform);
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
        if (player.inventory.items.Count > 23) { return; }

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


            case 4: if(bait.item != null) { break; }
                if (haczyk != null){       AddToInventory(haczyk, 0); }         haczyk = null;
                foreach (Transform child in fishingRod.components.haczyk.transform)
                {
                    Destroy(child.gameObject);
                }
                break;
            case 5:
                if (bait.item != null) { AddToInventory(bait.item, bait.amount); }
                bait.item = null;
                foreach (Transform child in fishingRod.components.bait.transform)
                {
                        Destroy(child.gameObject);
                }
                break;
            case 6:
                if (fishes.Count > 3) { break; }
                if (nest != null) { AddToInventory(nest, 0); }
                fishesCapacity = 4;
                nest = null;
                break;
        }
        AudioManager.instance.PlaySFX("popClose");
        afterShop();
    }
    public void openHelpBook()
    {
        if(player.tabManagement.tabTip.activeSelf) { player.tabManagement.tabTip.SetActive(false); }

        player.inventory.ui.gameObject.SetActive(false);
        player.inventory.opened = !player.inventory.opened;

        AudioManager.instance.PlaySFX("popClose");
        player.book.gameObject.SetActive(true);
        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

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
            if (eq.eqType == EquipmentObject.EquipmentType.Nest)
            {
                if (nest == null) { nest = eq; items.Remove(Object); fishesCapacity = nest.capacity; }
                else
                {
                    if (fishes.Count <= eq.capacity)
                    {
                        Debug.Log(fishes.Count+eq.capacity);
                        var ram = nest;
                        nest = eq;
                        items[slot - 1].item = ram;
                        fishesCapacity = nest.capacity;
                    }
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

                //HandleBaitSlotManagement(eq);
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
                if (!unlockedBaitSlots.activeSelf) { return; }
                //if (previous_bait > 2) { previous_bait = 0; }
                //if (!unlockedBaitSlots.activeSelf && previous_bait > 1){previous_bait = 0;}
                //if (!unlockedBaitSlots.activeSelf && previous_bait > 0){previous_bait = 0;}

                if(bait.item == eq) { bait.amount += amount; items.Remove(Object); }
                else if (bait.item == null) 
                { 
                    bait.item = eq; 
                    bait.amount = amount; 
                    items.Remove(Object); 
                    //previous_bait++; 
                }
                else
                {
                    var ram = bait.item;
                    var ram2 = bait.amount;

                    bait.item = eq;
                    bait.amount = amount;

                    items[slot - 1].item = ram;
                    items[slot - 1].amount = ram2;
                    //previous_bait++;
                }
            }
        }

        AudioManager.instance.PlaySFX("popClose");
        afterShop();
    }
    //public void HandleBaitSlotManagement(EquipmentObject haczyk)
    //{
    //    previous_bait = 0;
    //    foreach(GameObject x in unlockedBaitSlots)
    //    {
    //        x.SetActive(false);
    //    }
    //
    //    if(haczyk.power > -1) { unlockedBaitSlots[0].SetActive(true); }
    //    if(haczyk.power > 25) { unlockedBaitSlots[1].SetActive(true); }
    //    if(haczyk.power > 625) { unlockedBaitSlots[2].SetActive(true); }
    //
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (!unlockedBaitSlots[i].activeSelf && bait[i].item != null)
    //        {
    //            AddToInventory(bait[i].item, 1);
    //            bait[i] = null;
    //            ui.przynetyItems[i].GetComponent<UnityEngine.UI.Image>().sprite = null;
    //        }
    //    }
    //}
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
        if (a > -1)     { tier.SetActive(true); tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[0]; fishingRod.stats.rodTier = 0; }
        if (a > 99)     {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[1]; fishingRod.stats.rodTier = 1; }
        if (a > 999)    {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[2]; fishingRod.stats.rodTier = 2; }
        if (a > 2499)   {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[3]; fishingRod.stats.rodTier = 3; }
        if (a > 7499)   {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[4]; fishingRod.stats.rodTier = 4; }
        if (a > 29999)  {                       tier.GetComponent<UnityEngine.UI.Image>().sprite = fishingRod.stats.tiers[5]; fishingRod.stats.rodTier = 5; }

    }

    public void AddToInventory(ItemObject item, int amount)
    {
        if (amount == 0)
        {
            items.Add(new InventoryItem(item, 0));
        }
        else
        {
            foreach (InventoryItem x in items)
            {
                if (x.item == item)
                {
                    x.amount += amount;
                    return;
                }
            }
            items.Add(new InventoryItem(item, amount));
        }
    }

    public void TakeOneBait()
    {
        //for(int i = bait.Count-1;  i >= 0; i--)
        //{
            if (bait.item != null)
            {
                bait.amount -= 1;
                if (bait.amount < 1) { bait.item = null; bait.amount = 0; }
            }
        //}
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