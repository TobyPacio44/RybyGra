using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Version : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {       
    text.text = Application.version;
    }
}
