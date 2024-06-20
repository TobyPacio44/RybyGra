using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Teleports : MonoBehaviour
{
    public GameObject player;
    public Vision vision;
    public List<GameObject> points;
    public List<GameObject> buttons;

    public void UIOn(List<GameObject> teleports)
    {       
        points =new List<GameObject>(teleports);
        for (int i = 0;  i < points.Count; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponent<UnityEngine.UI.Image>().sprite = points[i].GetComponent<Bus>().sprite;
        }
        player.GetComponent<CharacterController>().enabled = false;
        vision.move = false;
        gameObject.SetActive(true);
    }

    public void UIOff()
    {
        points.Clear();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(false);
        }
        vision.move = true;
        gameObject.SetActive(false);
    }

    public void Teleport(int a)
    {
        player.transform.position = points[a].GetComponent<Bus>().origin.transform.position;
        player.GetComponent<CharacterController>().enabled = true;

        UIOff();
    }
}
