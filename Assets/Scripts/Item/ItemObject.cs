using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
public enum ItemType
{
    Fish,
    Item,
    Equipment
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public string name;
    public int price;
}
