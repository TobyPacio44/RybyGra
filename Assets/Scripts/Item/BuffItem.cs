using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Buff Object", menuName = "Items/Buff")]
public class BuffItem : ItemObject
{
    public enum BuffType
    {
        basic,
        strenghten,
        enchant,
        rarity,
        giants,
        findings
    }

    public BuffType buffType;

    [TextArea]
    public string description;
}
