using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Fish Object", menuName = "Items/Fish")]
public class FishObject : ItemObject
{
    [Header("(0-17)")]
    public int fishDifficulty;
    [Header("(0.1 - 500) kg")]
    [Range(0.1f, 500)]
    public float weight;

    [Header("Mno¿nik")]
    public int mnoznik;

    [Header("Przynêty")]
    public EquipmentObject likes;

    [Header("Tiery")]
    public List<bool> tiery;
}
