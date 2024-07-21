using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using static Player;
public class Dialogue : MonoBehaviour, IInteractable
{
    public FindingObject find;
    public NpcDialogueMan npcDialogueMan;

    public Sprite npcFace;
    public string npcName;
    [TextArea]
    public List<string> sentences;

    public bool animated;
    public Animator anim;
    public List<string> sentenceAnimation;
    public void Interact(Player player)
    {
        if(player.dialogueManager.Dialogue != null) { return; }
        npcDialogueMan.checkMission(player, this);
    }

    public void LoadDialogue(Player player) {
        player.dialogueManager.LoadDialogue(this);
        if(find != null)
        {
            int money = PlayerPrefs.GetInt("money");
            money += find.price;
            PlayerPrefs.SetInt("money", money);

            player.inventory.findings.Remove(find);
            foreach(InventoryItem item in player.inventory.items)
            {
                if(item.item.name == find.name)
                {
                    player.inventory.items.Remove(item);
                    return;
                }
            }
        }
    }
}
