using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuffDuration : MonoBehaviour
{
    public BuffManager buffManager;
    public bool active;
    public int time;
    public TextMeshProUGUI timeText;

    public GameObject on;
    public void StartCounting(GameObject On)
    {
        on = On;
        on.SetActive(true);
        StartCoroutine(Duration(time));
    }
    public IEnumerator Duration(int duration)
    {
        time = duration;
        active = true;
        while (true)
        {
            if(time < 1) {
                active = false;
                on.SetActive(false);
                buffManager.UpdateActives(); 
                yield break; 
            }
            timeText.text = time.ToString() ;
            yield return new WaitForSeconds(1);
            time -= 1;
        }
    }
}