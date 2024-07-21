using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StaticTip : MonoBehaviour
{
    public Image image;
    public static StaticTip instance;
    public TextMeshProUGUI uiText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        AddTip("Press [TAB] and open help book for a quick tutorial", 25);
    }
    public void AddTip(string text, float seconds)
    {
        if (uiText.text == text) { return; }
        image.gameObject.SetActive(true);
        uiText.text = text;
        StartCoroutine(pulse(seconds));
    }
    public IEnumerator pulse(float x)
    {
        for (int i = 0; i < x; i++)
        {
            image.enabled = true;
            yield return new WaitForSeconds(0.5f);
            image.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }

        uiText.text = "";
        image.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
