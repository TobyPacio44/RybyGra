using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class FishingInfoUI : MonoBehaviour
{
    public Player player;
    public FishingInfo info;
    public GameObject mapParent;

    public GameObject mapSection;
    public GameObject upgradeSection;
    public GameObject fishSection;

    public GameObject fishButton;
    public List<Transform> fishSlots;
    public List<GameObject> fishes;
    public TextMeshProUGUI spotName;

    public void switchToMap()
    {
        mapSection.SetActive(true);

        upgradeSection.SetActive(false);
        fishSection.SetActive(false);

        openMap();
    }
    public void switchToUpgrade()
    {
        upgradeSection.SetActive(true);

        mapSection.SetActive(false);
        fishSection.SetActive(false);
    }
    public void switchToFish()
    {
        fishSection.SetActive(true);

        mapSection.SetActive(false);
        upgradeSection.SetActive(false);
        openFish();
    }

    public void openMap()
    {
        if(mapParent.transform.childCount > 0) { return; }
        GameObject map = Instantiate(info.map,mapParent.transform);
        spotName.text = info.spotName;
        foreach(Transform child in map.transform)
        {
            child.GetComponent<Button>().onClick.AddListener(() => Teleport(int.Parse(child.name)));
            Debug.Log(int.Parse(child.name));
        }
        
    }

    public void openFish()
    {
        foreach(GameObject z in fishes)
        {
            Destroy(z);
        }

        int i = 0;
        foreach (FishObject x in info.list.list)
        {
            GameObject z = Instantiate(fishButton, fishSlots[i]);
            foreach(Transform child in z.transform)
            {
                if (child.name == "sprite1") { child.GetComponent<Image>().sprite = x.sprite; }
                if (child.name == "sprite2") { child.GetComponent<Image>().sprite = x.likes.sprite; }
                if (child.name == "nazwa") { child.GetComponent<TextMeshProUGUI>().text = x.name.ToString(); }
            }
            i++;
            fishes.Add(z);
        }
    }
    public void Teleport(int i)
    {
        Debug.Log(i);
        player.transform.position = info.teleports[i].transform.position;
        player.GetComponent<CharacterController>().enabled = true;
        switchToNormal();
    }

    public void switchToNormal()
    {
        player.GetComponent<CharacterController>().enabled = true;
        player.Screen.move = true;
        gameObject.SetActive(false);
    }
}
