using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialogueMan : MonoBehaviour
{
    public List<GameObject> dialogues;

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
}
