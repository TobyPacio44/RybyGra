using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public int points;
    public GameObject text;
    public void UpdateMoney()
    {
        points = PlayerPrefs.GetInt("money");
        text.GetComponent<TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(RepeatFunction());
    }

    private IEnumerator RepeatFunction()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            points = PlayerPrefs.GetInt("money");
            text.GetComponent<TextMeshProUGUI>().text = points.ToString();
        }
    }
    
}
