using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using static Player;

public class Bus : MonoBehaviour, IInteractable
{
    public Sprite sprite;
    public List<GameObject> teleports;
    public Transform origin;
    public GameObject disable;

    public string musicName;
    public void Interact(Player player)
    {
        player.tel.UIOn(teleports);
    }

}
