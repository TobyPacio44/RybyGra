using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Skup : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        if (player.skupUI.gameObject.active == false)
        {
            player.skupUI.CreateList();
            player.skupUI.gameObject.SetActive(true);
        }
    }
}
