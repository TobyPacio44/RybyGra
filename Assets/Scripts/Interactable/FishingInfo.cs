using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;
public class FishingInfo : MonoBehaviour, IInteractable
{
    public string spotName;
    public FishList list;
    public GameObject map;
    public GameObject upgradeMap;
    public List<FishObject> fishes;
    public List<Build> upgrades;

    public List<Transform> teleports;

    public void Interact(Player player)
    {
        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;
        player.fishingInfoUI.gameObject.SetActive(true);

        player.fishingInfoUI.info = this;
        player.fishingInfoUI.switchToMap();
    }
}
