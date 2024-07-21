using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Finding Object", menuName = "Items/Finding")]
public class FindingObject : ItemObject
{
    [Header("Message")]
    public int id;
    public GameObject message;
    public bool unique;
}
