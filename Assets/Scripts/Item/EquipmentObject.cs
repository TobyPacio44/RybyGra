using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public enum EquipmentType
    {
        Kij,
        Kolowrotek,
        Zylka,
        Haczyk,
        Splawik,
        Przyneta
    }

    public EquipmentType eqType;
    public int power;
}
