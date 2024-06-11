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

    public List<EquipmentObject> bait = new List<EquipmentObject>();

    private bool opened;
    private void Start()
    {
        afterShop();
        money.UpdateMoney();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
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
        }
        for (int i = 0; i < items.Count; i++)
        {
            var element = ui.itemsItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = items[i].sprite;
        }
    }
    public void UpdateEquipment()
    {
        if (kij != null) { ui.fishRodItems[0].GetComponent<UnityEngine.UI.Image>().sprite = kij.sprite; }
        if (kolowrotek != null) { ui.fishRodItems[1].GetComponent<UnityEngine.UI.Image>().sprite = kolowrotek.sprite; }
        if (zylka != null) { ui.fishRodItems[2].GetComponent<UnityEngine.UI.Image>().sprite = zylka.sprite; }
        if (haczyk != null) { ui.fishRodItems[3].GetComponent<UnityEngine.UI.Image>().sprite = haczyk.sprite; }
        if (splawik != null) { ui.fishRodItems[4].GetComponent<UnityEngine.UI.Image>().sprite = splawik.sprite; }
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
        if (Object is EquipmentObject)
        {
            var eq = (EquipmentObject)Object;

            if (eq.eqType == EquipmentObject.EquipmentType.Kij)
            {
                if (kij == null) { kij = eq; items.Remove(Object); }
                else
                {
                    var ram = kij;
                    kij = eq;
                    items[slot - 1] = ram;
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
                    items[slot - 1] = ram;
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
                    items[slot - 1] = ram;
                }
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Haczyk)
            {
                if (haczyk == null) { haczyk = eq; items.Remove(Object); }
                else
                {
                    var ram = haczyk;
                    haczyk = eq;
                    items[slot - 1] = ram;
                }

                InstantiateRod(fishingRod.components.haczyk, eq);
            }
            if (eq.eqType == EquipmentObject.EquipmentType.Splawik)
            {
                if (splawik == null) { splawik = eq; items.Remove(Object); }
                else
                {
                    var ram = splawik;
                    splawik = eq;
                    items[slot - 1] = ram;
                }
            }
        }

        afterShop();
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
}