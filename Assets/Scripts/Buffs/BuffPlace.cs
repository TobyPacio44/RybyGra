using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
public class BuffPlace : MonoBehaviour, IInteractable
{
    public FishList fishList;
    public List<BuffItem> buffList;

    public void Interact(Player player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

        player.buffManager.ui.gameObject.SetActive(true);
        player.buffManager.place = this;
        player.buffManager.buffs = buffList;
        player.buffManager.UpdateBuffUI();
    }
}
