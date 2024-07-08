using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkupUI : MonoBehaviour
{
    public Player player;
    public GameObject buttonPrefab;
    public List<GameObject> slots;

    public List<FishObject> fishes;

    public TextMeshProUGUI money;
    public TextMeshProUGUI moneyFish;

    public void ClearList()
    {
        foreach (GameObject x in slots)
        {
            foreach (Transform child in x.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    public void CreateList()
    {
        fishes = player.inventory.fishes;

        CalculateMoney();
        ClearList();


        player.GetComponent<CharacterController>().enabled = false;
        player.Screen.move = false;

        int i = 0;
        foreach (FishObject fish in fishes)
        {
            GameObject newButton = Instantiate(buttonPrefab, slots[i].transform);
            i++;
            newButton.GetComponent<Button>().onClick.AddListener(() => SellFish(fish));
            foreach (Transform child in newButton.transform)
            {
                if (child.name == "Sprite") { child.GetComponent<Image>().sprite = fish.sprite; }
                if (child.name == "Cost") { child.GetComponent<TextMeshProUGUI>().text = fish.price.ToString(); }
            }
        }
    }

    public void CalculateMoney()
    {
        money.text = PlayerPrefs.GetInt("money").ToString();

        int ram = 0;
        foreach (FishObject fish in fishes)
        {
            ram += fish.price;
        }
        moneyFish.text = ram.ToString();
    }
    public void SellFish(FishObject fish)
    {
        int money = PlayerPrefs.GetInt("money");
        money += fish.price;
        PlayerPrefs.SetInt("money", money);
        player.inventory.fishes.Remove(fish);
        CreateList();
    }

    public void SellAll()
    {
        int money = PlayerPrefs.GetInt("money");
        foreach(FishObject fish in fishes)
        {
            money += fish.price;           
        }
        PlayerPrefs.SetInt("money", money);
        player.inventory.fishes.Clear();
        CreateList();
    }

    public void Close(GameObject x)
    {
        x.SetActive(false);
        player.GetComponent<CharacterController>().enabled = true;
        player.Screen.move = true;
    }
}
