using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Player player;
    public int number;
    public Transform pageParent;
    public GameObject page;
    public GameObject[] pages;

    public void turnPage(int a)
    {
        Destroy(page);
        number += a;
        if(number >= pages.Length) { number = 0; } 
        if(number < 0) { number = pages.Length-1; }

        page = Instantiate(pages[number], pageParent);
    }

    public void closeBook()
    {
        gameObject.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        player.Screen.move = true;
    }
}
