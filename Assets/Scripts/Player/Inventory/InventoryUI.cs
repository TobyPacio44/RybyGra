using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public GameObject fishingRod;
    public List<GameObject> fishesItems = new List<GameObject>();
    public List<GameObject> itemsItems = new List<GameObject>();
    public List<GameObject> zan�tyItems = new List<GameObject>();
    public List<GameObject> przyn�tyItems = new List<GameObject>();
    public List<GameObject> fishRodItems = new List<GameObject>();
    public List<Sprite> fishRodItemsPlaceHolders = new List<Sprite>();
    public TextMeshProUGUI rodPower;
}
