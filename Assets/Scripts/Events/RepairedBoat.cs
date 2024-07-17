using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairedBoat : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("repairedBoat");

        RonStatic.instance.GetComponent<NpcDialogueMan>().ChangeDialogue("DialAfterBoat");
        DonStatic.instance.GetComponent<NpcDialogueMan>().ChangeDialogue("DialAfterBoat");
        GarryStatic.instance.GetComponent<NpcDialogueMan>().ChangeDialogue("DialAfterBoat");
        RandyStatic.instance.GetComponent<NpcDialogueMan>().ChangeDialogue("DialAfterBoat");
        BillyStatic.instance.GetComponent<NpcDialogueMan>().ChangeDialogue("DialAfterBoat");
    }
}
