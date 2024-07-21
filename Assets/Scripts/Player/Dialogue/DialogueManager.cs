using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;
public class DialogueManager : MonoBehaviour
{
    public Image npcFace;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI sentence;
    public Dialogue Dialogue;

    public int currentSentence = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) { currentSentence++; NextSentence(); }
    }
    public void LoadDialogue(Dialogue dialogue)
    {
        Dialogue = dialogue;
        Dialogue.gameObject.GetComponent<Collider>().enabled = false;
        if (Dialogue.sentences.Count > 0)
        {
            currentSentence = 0;
            gameObject.SetActive(true);
            npcFace.sprite = Dialogue.npcFace;
            npcName.text = Dialogue.npcName;
            NextSentence();
        }
    }

    public void NextSentence()
    {
        AudioManager.instance.PlaySFX("popClose");
        if (Dialogue == null) { gameObject.SetActive(false); return; }

        if (currentSentence>Dialogue.sentences.Count-1) { gameObject.SetActive(false); 
        Dialogue.gameObject.GetComponent<Collider>().enabled = true;
            Dialogue = null;
            return; 
        }

        sentence.text = Dialogue.sentences[currentSentence];

        if (Dialogue.animated)
        {
            Dialogue.anim.Play(Dialogue.sentenceAnimation[Random.Range(0, Dialogue.sentenceAnimation.Count)]);
        }
    }
}
