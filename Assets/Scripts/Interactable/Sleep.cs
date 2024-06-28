using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
public class Sleep : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        GameManager.Instance.hour += 1;
    }
}
