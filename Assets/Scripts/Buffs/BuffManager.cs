using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

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
    public void UpdateBuffUI()
    {
        if(activeBuff != null) { return; }
        foreach(GameObject x in buffButtons)
        {
            Destroy(x);
        }
        buffButtons.Clear();     

        int i = 0;
        foreach (BuffItem item in buffs)
        {
            GameObject buff = Instantiate(buffObject, buffSlots[i]);
            foreach (Transform child in buff.transform)
            {
                if (child.name == "Description") { child.GetComponent<TextMeshProUGUI>().text = item.description; }
                if (child.name == "Name") { child.GetComponent<TextMeshProUGUI>().text = item.name; }
                if (child.name == "Sprite") { child.GetComponent<UnityEngine.UI.Image>().sprite = item.sprite; }
            }
            buffButtons.Add(buff);
            i++;
        }
    }

    public void ActivateBuff(int i)
    {
        if(activeBuff != null) { return; }
        i--;
        GameObject active = Instantiate(activeObject, activeSlots[0]);
        foreach (Transform child in active.transform)
        {
            if (child.name == "Name") { child.GetComponent<TextMeshProUGUI>().text = buffs[i].name; }
            if (child.name == "Sprite") { child.GetComponent<UnityEngine.UI.Image>().sprite = buffs[i].sprite; }          
        }
        active.GetComponent<BuffDuration>().buffManager = this;
        activeBuff = active.GetComponent<BuffDuration>();
        UpdateBuffUI();
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
}
