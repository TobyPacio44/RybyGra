using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Tip : MonoBehaviour, IInteractable
{
    [TextArea]
    public string text;
    public float seconds;
    public void Interact(Player player)
    {
        StaticTip.instance.AddTip(text, seconds);
    }  
}
