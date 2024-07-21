using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public Player player;
    public BuffPlace place;
    public GameObject buffObject;
    public GameObject activeObject;
    public GameObject ui;

    public List<BuffItem> buffs;
    public List<GameObject> buffButtons;
    public BuffDuration activeBuff;
    public List<Transform> buffSlots;
    public List<Transform> activeSlots;
    public TextMeshProUGUI spotName;
    public void UpdateBuffUI()
    {
        spotName.text = place.fishList.info.spotName;
        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

        if (activeBuff != null) { return; }
        foreach(GameObject x in buffButtons)
        {
            Destroy(x);
        }
        buffButtons.Clear();     

        int i = 0;
        foreach (BuffItem item in buffs)
        {
            GameObject buff = Instantiate(buffObject, buffSlots[i]);
            int playerHolding = 0;

            foreach(InventoryItem x in player.inventory.items)
            {
                if(x.item.name == item.name) { playerHolding += 1; } 
            }

            foreach (Transform child in buff.transform)
            {
                if (child.name == "Description") { child.GetComponent<TextMeshProUGUI>().text = item.description; }
                if (child.name == "Name") { child.GetComponent<TextMeshProUGUI>().text = item.name; }
                if (child.name == "Sprite") { child.GetComponent<UnityEngine.UI.Image>().sprite = item.sprite; }
                if (child.name == "Amount") { child.GetComponent<TextMeshProUGUI>().text = playerHolding.ToString(); }
            }
            buffButtons.Add(buff);
            i++;
        }
    }

    public void ActivateBuff(int i)
    {
        if(activeBuff != null) { return; }
        i--;

        bool conti = false;
        foreach(InventoryItem y in player.inventory.items)
        {
            if(y.item == buffs[i])
            {
                player.inventory.items.Remove(y);
                UpdateBuffUI();
                conti = true;
                break;
            }
        }
        if (!conti) { return; }
            
        GameObject active = Instantiate(activeObject, activeSlots[0]);
        foreach (Transform child in active.transform)
        {
            if (child.name == "Name") { child.GetComponent<TextMeshProUGUI>().text = buffs[i].name; }
            if (child.name == "Sprite") { child.GetComponent<UnityEngine.UI.Image>().sprite = buffs[i].sprite; }          
        }
        active.GetComponent<BuffDuration>().buffManager = this;
        activeBuff = active.GetComponent<BuffDuration>();
        place.fishList.buff = buffs[i];
        active.GetComponent<BuffDuration>().StartCounting(buffButtons[i].transform.Find("On").gameObject);        
    }

    public void UpdateActives()
    {
            if (activeBuff.active == false)
            {
                Destroy(activeBuff.gameObject);
                activeBuff = null;
                place.fishList.buff = null;
            }
        
    }

    public void switchToNormal()
    {
        player.GetComponent<CharacterController>().enabled = true;
        player.Screen.move = true;
        ui.SetActive(false);
    }
}
