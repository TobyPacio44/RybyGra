using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FishList : MonoBehaviour
{
    public FishingInfo info;
    public List<FishObject> list;
    public List<FindingObject> findings;

    public FishObject defaultFish;
    public float baseChanceToHook;
    public float offChanceToHook;
    public bool activeHours;
    public int baseFindingChance;

    [HideInInspector]
    public int findingsChance;

    public BuffItem buff;   
    private List<FishObject> ram = new List<FishObject>();
    public List<FishObject> eligibleFish = new List<FishObject>();

    public IEnumerator updateChance(FishingRod rod)
    {
        while (true)
        {
            if (activeHours)
            {
                rod.stats.chanceToHook = baseChanceToHook;
            }
            else
            {
                rod.stats.chanceToHook = offChanceToHook;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void HandleFishSelection(FishingRod rod, InventoryItem bait)
    {
        StartCoroutine(updateChance(rod));

        ram.Clear();
        eligibleFish.Clear();

        foreach (FishObject fish in list){
            if (rod.stats.RodPower > -1) { if (fish.fishDifficulty < 6) { ram.Add(fish); continue; } }
            if (rod.stats.RodPower > 99) { if (fish.fishDifficulty < 9) { ram.Add(fish); continue; } }
            if (rod.stats.RodPower > 999) { if (fish.fishDifficulty < 12) { ram.Add(fish); continue; } }
            if (rod.stats.RodPower > 2499) { if (fish.fishDifficulty < 19) { ram.Add(fish); continue; } }
            if (rod.stats.RodPower > 7499) { ram.Add(fish); continue; }
        }

        foreach(FishObject fish in ram)
        {
            eligibleFish.Add(FishObject.Instantiate(fish));
        }

        foreach (FishObject fish in ram) {
            //foreach(InventoryItem x in bait) {
                if (bait.item == fish.likes) {
                    eligibleFish.Add(FishObject.Instantiate(fish)); 
                    eligibleFish.Add(FishObject.Instantiate(fish)); 
                }
            //}
        }

        findingsChance = baseFindingChance;

        if (buff == null)
        {
            return; 
        }
        else
        {
            ram.Clear();
            foreach(FishObject fish in eligibleFish)
            {
                ram.Add(fish);
            }
            eligibleFish.Clear();

            if (buff.buffType == BuffItem.BuffType.basic)
            {
                rod.stats.chanceToHook = baseChanceToHook + 15;

                int i = 0;
                foreach (FishObject fish in ram)
                {
                    eligibleFish.Add(FishObject.Instantiate(fish));
                    i++;
                }
            }

            if (buff.buffType == BuffItem.BuffType.enchant)
            {
                rod.stats.chanceToHook = baseChanceToHook + 5;

                int i = 0;
                foreach (FishObject fish in ram)
                {
                    eligibleFish.Add(FishObject.Instantiate(fish));
                    float price = eligibleFish[i].price;
                    price = price + (price * 0.25f);
                    eligibleFish[i].price = Convert.ToInt32(price);
                    i++;
                }
            }

            if (buff.buffType == BuffItem.BuffType.findings)
            {
                findingsChance = baseFindingChance + 30;
            }

            if(eligibleFish.Count == 0)
            {
                foreach (FishObject fish in ram)
                {
                    eligibleFish.Add(fish);
                }
            }
        }
    }
}
