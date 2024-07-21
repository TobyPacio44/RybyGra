using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Equals))
        {
            AddMoney();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Minus))
        {
            SkipDay();
        }
    }
    public void AddMoney()
    {
        int money = PlayerPrefs.GetInt("money");
        money += 100;
        PlayerPrefs.SetInt("money", money);
    }

    public void SkipDay()
    {
        if (GameManager.Instance.day != null)
        {
            GameManager.Instance.hour += 1;
            float cut = GameManager.Instance.hour - 5;
            GameManager.Instance.day.GetComponent<Animator>().Play("Sun", 0, cut / 17);
        }
        else
        {
            GameManager.Instance.hour += 1;
        }
    }
}
