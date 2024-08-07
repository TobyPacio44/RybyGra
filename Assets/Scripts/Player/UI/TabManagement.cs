using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManagement : MonoBehaviour
{
    public Player player;
    public GameObject tabTip;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
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
                FollowMouse.instance.gameObject.SetActive(false);
                player.inventory.ui.gameObject.SetActive(false);
                player.inventory.opened = !player.inventory.opened;
            } 
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
                player.fishingInfoUI.gameObject.SetActive(false);
                player.buffManager.ui.gameObject.SetActive(false);
                player.ShopUI.gameObject.SetActive(false);
                player.skupUI.gameObject.SetActive(false);
                player.choice.gameObject.SetActive(false);
                player.tel.gameObject.SetActive(false);
                player.accept.gameObject.SetActive(false);
                player.book.gameObject.SetActive(false);

            if (!player.inventory.opened && !player.options.gameObject.activeSelf)
            {
                player.Screen.move = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.inventory.ui.gameObject.SetActive(false);
            player.inventory.opened = !player.inventory.opened;

            if (!player.options.gameObject.activeSelf)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.Screen.move = false;
                player.options.gameObject.SetActive(true);
            }
            else
            {
                player.GetComponent<CharacterController>().enabled = true;
                player.Screen.move = true;
                FollowMouse.instance.gameObject.SetActive(false);
                player.options.gameObject.SetActive(false);
            }
        }
    }
}
