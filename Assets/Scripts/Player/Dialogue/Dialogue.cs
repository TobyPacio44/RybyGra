using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Player;
public class Dialogue : MonoBehaviour, IInteractable
{
    public Sprite npcFace;
    public string npcName;
    [TextArea]
    public List<string> sentences;

    public bool animated;
    public Animator anim;
    public List<string> sentenceAnimation;
    public void Interact(Player player)
    {
        player.dialogueManager.LoadDialogue(this);
    }
}
