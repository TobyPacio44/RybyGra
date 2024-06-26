using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManagement : MonoBehaviour
{
    public Player player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (player.fishingInfoUI.gameObject.activeSelf == true)
            {
                player.fishingInfoUI.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }

            if (player.buffManager.ui.activeSelf == true)
            {
                player.buffManager.ui.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }
            if (player.ShopUI.gameObject.activeSelf == true)
            {
                player.ShopUI.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }
            if (player.skupUI.gameObject.activeSelf == true)
            {
                player.skupUI.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }
            if (player.choice.gameObject.activeSelf == true)
            {
                player.choice.gameObject.SetActive(false);
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                return;
            }

            if (!player.inventory.opened)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.Screen.move = false;
                player.inventory.ui.gameObject.SetActive(true);
                player.inventory.UpdateEq();
                player.inventory.opened = !player.inventory.opened;
            }
            else
            {
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                player.inventory.ui.gameObject.SetActive(false);
                player.inventory.opened = !player.inventory.opened; 
            }

        }
    }
}
