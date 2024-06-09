using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Holder holder;
    public FishingMinigame minigame;
    public Inventory inventory;
    public ShopUI ShopUI;
    public Teleports tel;
    public Accept accept;

    public Vision Screen;
    public Transform cam;
    private void Update()
    {
        RaycastHit hit;

        if (!Physics.Raycast(transform.position, transform.forward, 2.5f))
        {
            Screen.reticle.gameObject.SetActive(false);
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.5f))
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
