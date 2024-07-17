using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
public class Sleep : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        if(GameManager.Instance.hour > 18 || GameManager.Instance.hour < 5)
        {
            GameManager.Instance.StartDay();
        }
    }
}
