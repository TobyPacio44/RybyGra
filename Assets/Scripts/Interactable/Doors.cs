using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class Doors : MonoBehaviour, IInteractable
{
        public Quaternion Open = Quaternion.Euler(0, 0, 0);
        private Quaternion Closed;
        public Collider disable;
        public bool isOpened;



        private void Start()
        {
            Closed = transform.rotation;
            isOpened = false;
        }

        public void Interact(Player player)
        {
                if (!isOpened) { isOpened = true; }
                else { isOpened = false; }
        }
        private void Update()
        {
            if (isOpened)
            {
                OpenUp();
            }

            if (!isOpened)
            {
                CloseDown();
            }
        }

        private void OpenUp()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Closed * Open, 0.10f);

            if (disable != null) { disable.enabled = false; }
        }

        private void CloseDown()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Closed, 0.10f);

            if (disable != null) { disable.enabled = true; }
        }
}
