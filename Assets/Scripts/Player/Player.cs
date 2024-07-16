using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Holder holder;
    public MiniGame minigame;
    public Inventory inventory;
    public Choice choice;
    public ShopUI ShopUI;
    public SkupUI skupUI;
    public BuffManager buffManager;
    public Teleports tel;
    public Accept accept;
    public FishingInfoUI fishingInfoUI;
    public Book book;
    public Options options;
    public TabManagement tabManagement;
    public DialogueManager dialogueManager;

    public Vision Screen;
    public Transform cam;

    public LayerMask IgnoreMe;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Update()
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, 3f, ~IgnoreMe))
        {
            Screen.reticle.gameObject.SetActive(false);
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3f, ~IgnoreMe))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Screen.reticle.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(this);
                }
            }
        }
    }
    public interface IInteractable
    {
        void Interact(Player player);
    }
}
