using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Items/Fish")]
public class FishObject : ItemObject
{
    [Header("(0-17)")]
    public int fishDifficulty;
    //(0.1 - 500) kg
    public float weight;

    //[Header("Mno¿nik")]
    public float mnoznik;

    [Header("Przynêty")]
    public EquipmentObject likes;

    [Header("Tiery")]
    public List<bool> tiery;
    public int tier;
}
