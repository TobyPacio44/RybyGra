using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcDialogueMan : MonoBehaviour
{
    public List<GameObject> dialogues;

    private void Awake()
    {
        foreach (GameObject x in dialogues)
        {
            x.GetComponent<Dialogue>().npcDialogueMan = this;
        }
    }
    public void ChangeDialogue(string name)
    {
        foreach (GameObject dialogue in dialogues)
        {
            dialogue.gameObject.SetActive(false);
        }

        foreach (GameObject dialogue in dialogues)
        {
            if (dialogue.name == name)
            {
                dialogue.SetActive(true);
            }
        }
    }

    public void checkMission(Player player, Dialogue dialogue)
    {
        foreach(FindingObject x in player.inventory.findings)
        {
            foreach(GameObject z in dialogues)
            {
                if(x == z.GetComponent<Dialogue>().find)
                {
                    z.GetComponent<Dialogue>().LoadDialogue(player);
                    return;
                }
            }
        }
            dialogue.LoadDialogue(player);
    }
}
