using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class readTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    void Update()
    {
        timeText.text = GameManager.Instance.hour.ToString();
    }
}
