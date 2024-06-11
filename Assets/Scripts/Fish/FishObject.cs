using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Items/Fish")]
public class FishObject : ItemObject
{
    [Header("(0-17)")]
    public int fishDifficulty;
    [Header("(1-4)")]
    public int weight;

    public EquipmentObject likes;
}
