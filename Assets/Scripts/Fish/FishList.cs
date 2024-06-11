using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FishList : MonoBehaviour
{
    public List<FishObject> list;

    public BuffItem buff;
    public List<FishObject> eligibleFish = new List<FishObject>();

    public void HandleFishSelection(int rodPower, List<EquipmentObject> bait)
    {
        foreach (FishObject fish in list){
            if (rodPower > 0) { if (fish.fishDifficulty < 6) { eligibleFish.Add(fish); continue; } }
            if (rodPower > 99) { if (fish.fishDifficulty < 9) { eligibleFish.Add(fish); continue; } }
            if (rodPower > 999) { if (fish.fishDifficulty < 12) { eligibleFish.Add(fish); continue; } }
            if (rodPower > 2499) { if (fish.fishDifficulty < 19) { eligibleFish.Add(fish); continue; } }
            if (rodPower > 7499) { eligibleFish.Add(fish); continue; }
        }

        foreach (FishObject fish in eligibleFish) {
            bool iLike = false;
            foreach(EquipmentObject x in bait) {
                if (x == fish.likes) {  iLike = true; break; }
            }
            if (iLike == true) { continue; }
            else
            {
                eligibleFish.Remove(fish);
            }
        }

        if (buff == null) {
            return;
        }

        //Handle Buffs
    }
}
