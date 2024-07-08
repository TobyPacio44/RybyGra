using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingInfoUI : MonoBehaviour
{
    public Player player;
    public FishingInfo info;
    public GameObject mapParent;
    public GameObject upgradeMapParent;

    public GameObject mapSection;
    public GameObject upgradeSection;
    public GameObject fishSection;

    public GameObject fishButton;
    public List<Transform> fishSlots;
    public List<GameObject> fishes;
    public TextMeshProUGUI spotName;
    public TextMeshProUGUI hour1;
    public TextMeshProUGUI hour2;

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

        openUpgrades();
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
        }
        hour1.text = info.hour1.ToString();
        hour2.text = info.hour2.ToString();
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
    public void openUpgrades()
    {
        if (upgradeMapParent.transform.childCount > 1) { return; }
        GameObject map = Instantiate(info.upgradeMap, upgradeMapParent.transform);
        int i = 0;
        foreach (Transform child in map.transform)
        {
            child.GetComponent<Button>().onClick.AddListener(() => Build(int.Parse(child.name), child.GetChild(0).gameObject));
            child.GetChild(0).GetComponent<TextMeshProUGUI>().text = info.upgrades[i].price.ToString();
            i++;
        }
    }

    public void Build(int i, GameObject button)
    {
        info.upgrades[i].build(button);
    }
    public void Teleport(int i)
    {
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
