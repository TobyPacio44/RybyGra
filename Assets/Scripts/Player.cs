using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Vision Screen;
    public Holder holder;
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
            Screen.reticle.gameObject.SetActive(true);
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {
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
