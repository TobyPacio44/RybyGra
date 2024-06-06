using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Accept : MonoBehaviour
{
    public GameObject Sprite;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Price;

    public void SetAcceptUI(Sprite sprite, string name, int price)
    {
        Sprite.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        Text.text = name;
        Price.text = price.ToString();
    }
}
