using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FishingMinigame;
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Inventory/Items/Fish")]
public class FishObject : ItemObject
{
    [Header("(0-17)")]
    public int fishDifficulty;
    [Header("(1-4)")]
    public int weight;
}
