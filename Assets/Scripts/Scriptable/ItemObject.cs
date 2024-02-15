using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Fish,
    Item,
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public string name;
    public int price;
}
