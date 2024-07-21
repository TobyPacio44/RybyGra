using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Items/Fish")]
public class FishObject : ItemObject
{
    [Header("Fish")]
    public int fishDifficulty;
    public float weight;
    public float mnoznik;

    [Header("Przynêty")]
    public EquipmentObject likes;

    [Header("Tiery")]
    public List<bool> tiery;
    public int tier;
}
