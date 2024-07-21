using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
public enum ItemType
{
    Fish,
    Item,
    Equipment,
    Buff
}
public abstract class ItemObject : ScriptableObject
{
    [Header("Item")]
    public Sprite sprite;
    public GameObject prefab;
    public string name;
    public int price;
    public bool blocked;
}
