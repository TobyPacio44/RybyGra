using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
public enum ItemType
{
    Fish,
    Item,
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public ItemType type;
    public string name;
    public int price;
}
