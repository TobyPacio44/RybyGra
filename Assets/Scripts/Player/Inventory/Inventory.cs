using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventoryUI ui;

    public Money money;
    public GameObject fishingRod;
    public List<ItemObject> items = new List<ItemObject>();
    public List<ItemObject> fishes = new List<ItemObject>();
    public List<ItemObject> zanêty = new List<ItemObject>();
    public List<ItemObject> przynêty = new List<ItemObject>();

    private void Start()
    {
        money.UpdateMoney();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ui.gameObject.SetActive(true);
            UpdateEq();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            ui.gameObject.SetActive(false);
        }
    }
    public void UpdateEq()
    {

        foreach(GameObject x in ui.fishesItems)
        {
            x.SetActive(false);
        }
        for(int i  = 0; i < fishes.Count; i++)
        {
            var element = ui.fishesItems[i];
            element.SetActive(true);
            element.GetComponent<UnityEngine.UI.Image>().sprite = fishes[i].sprite;
        }
    }
}
