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
    public TextMeshProUGUI Weight;

    public void SetAcceptUI(Sprite sprite, string name, int price, float weight, bool fish)
    {
        if (fish)
        {
            Price.gameObject.SetActive(true); 
            Weight.gameObject.SetActive(true); 
            Weight.text = weight.ToString() + "kg"; 
            Price.text = price.ToString();
        }
        else
        {
            Price.gameObject.SetActive(false); 
            Weight.gameObject.SetActive(false);
        }

        Sprite.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        Text.text = name;        
    }
}
