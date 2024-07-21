using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemHover : MonoBehaviour, IPointerExitHandler, IPointerMoveHandler
{
    public ItemObject item;

    /*public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && !item.blocked)
        {
            FollowMouse.instance.gameObject.SetActive(true);
            FollowMouse.instance.transform.position = transform.position;
            FollowMouse.instance.text.text = item.name;
        }
        else
        {
            FollowMouse.instance.gameObject.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && !item.blocked)
        {
            FollowMouse.instance.gameObject.SetActive(true);
            FollowMouse.instance.transform.position = transform.position;
            FollowMouse.instance.text.text = item.name;
        }
    }*/
    public void OnPointerExit(PointerEventData eventData)
    {
        FollowMouse.instance.gameObject.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (item is FishObject)
        {
            var fish = (FishObject)item;
            FollowMouse.instance.gameObject.SetActive(true);
            FollowMouse.instance.transform.position = transform.position;
            FollowMouse.instance.text.text = fish.name+ "<br>Weight: " + fish.weight + " kg <br>Price: " + fish.price;
            return;
        }

        if (item != null && !item.blocked)
        {
            FollowMouse.instance.gameObject.SetActive(true);
            FollowMouse.instance.transform.position = transform.position;
            FollowMouse.instance.text.text = item.name;
        }
        else
        {
            FollowMouse.instance.gameObject.SetActive(false);
        }
    }
}
