using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public bool canFish;

    public Player player;
    public MiniGame minigame;
    public FishingRodStats stats;
    public FishingRodComponents components;
    public GameObject fishSpawn;

    [HideInInspector]
    public state State;
    public enum state
    {
        idle,cast,hooking,
    }

    [HideInInspector] public GameObject floatObject;

    public FishList fishList;
    public bool holdingFish;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!canFish) {
                StaticTip.instance.AddTip("Equip all rod components to fish.", 5);
                return; }
            if (holdingFish) { return; }
            if (player.inventory.fishesCapacity <= player.inventory.fishes.Count) {
                StaticTip.instance.AddTip("Fish inventory is full. Visit Billy.", 5);
                return; };
            if (State == state.cast) { Hooked(false, null); return; }

            foreach (Transform child in components.haczyk.transform)
            {
                child.gameObject.SetActive(false);
            }
            foreach (Transform child in components.bait.transform)
            {
                child.gameObject.SetActive(false);
            }

          
            StartCoroutine(Throw());
        }
    }
    public int RandomTick()
    {
        return Random.Range(0, 100);
    }

    public FishObject SelectFish()
    {
        if (fishList.eligibleFish.Count > 0)
        {
            FishObject selected = fishList.eligibleFish[Random.Range(0, fishList.eligibleFish.Count)];

            bool a = false;
            while (!a)
            {
                int z = Random.Range(0, 5);
                if (selected.tiery[z] == true)
                {
                    switch (z)
                    {
                        case 0: selected.tier = 0; selected.weight = Random.Range(0.1f, 0.5f  ); break;
                        case 1: selected.tier = 1; selected.weight = Random.Range(0.6f, 1.5f  ); break;
                        case 2: selected.tier = 2; selected.weight = Random.Range(1.6f, 3f    ); break;
                        case 3: selected.tier = 3; selected.weight = Random.Range(3.1f, 10f   ); break;
                        case 4: selected.tier = 4; selected.weight = Random.Range(10.1f,50f   ); break;
                    }

                    selected.weight = (Mathf.Round(selected.weight * 100)) / 100;

                    selected.price = selected.price + (int)(selected.weight * selected.mnoznik);
                    Debug.Log("waga: "+selected.weight);

                    a = true;
                }
            }

            return selected;
        }
        else
        {
            return null;
        }
    }
    public void Hooked(bool caught, ItemObject fish)
    {        
        foreach (Transform child in components.haczyk.transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in components.bait.transform)
        {
            child.gameObject.SetActive(true);
        }

        //minigame.parent.SetActive(false);
        State = state.idle;
        Destroy(floatObject);
        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);
        if (fishList != null) { fishList.StopAllCoroutines(); }               
        StopAllCoroutines();

        if (caught && fish != null)
        {
            if (fish is FishObject)
            {
                var Fish = fish as FishObject;

                player.accept.gameObject.SetActive(true);
                player.accept.SetAcceptUI(Fish.sprite, Fish.name, Fish.price, Fish.weight, true);
                holdingFish = true;
                var instantiate = Instantiate(Fish.prefab, fishSpawn.transform);
                StartCoroutine(takeFish(Fish, instantiate));
                player.inventory.TakeOneBait();
                fishList = null;
            }
            if (fish is FindingObject)
            {
                var Fish = fish as FindingObject;
                player.accept.gameObject.SetActive(true);
                player.accept.SetAcceptUI(Fish.sprite, Fish.name, Fish.price, 0, false);
                holdingFish = true;
                var instantiate = Instantiate(Fish.prefab, fishSpawn.transform);
                StartCoroutine(takeItem(Fish, instantiate));
            }      
        }
    }
    IEnumerator takeFish(FishObject fish, GameObject fishObject)
    {
        while (holdingFish)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.inventory.fishes.Add(fish);
                holdingFish = false;
                Destroy(fishObject);
                player.accept.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                holdingFish = false;
                Destroy(fishObject);
                player.accept.gameObject.SetActive(false);
            }
            yield return null;
        }
    }
    IEnumerator takeItem(FindingObject fish, GameObject fishObject)
    {
        while (holdingFish)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (fish.unique) { fishList.findings.Remove(fish); player.inventory.findings.Add(fish); }
                player.inventory.AddToInventory(fish, 0);
                holdingFish = false;
                Destroy(fishObject);
                player.accept.gameObject.SetActive(false); 
                fishList = null;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                holdingFish = false;
                Destroy(fishObject);
                player.accept.gameObject.SetActive(false);
                fishList = null;
            }
            yield return null;
        }
    }
    IEnumerator Throw()
    {

        if (State == state.hooking) { yield break; }
        GetComponent<Animation>().Play("RodAnimation");
        yield return new WaitForSeconds(0.45f);
        AudioManager.instance.PlaySFX("Throw");
        ThrowFloat();
        State = state.cast;

        yield return StartCoroutine(WaitForFishList());
        yield return StartCoroutine(Cast());
    }
    IEnumerator Cast()
    {
        fishList.HandleFishSelection(this, player.inventory.bait);
        //Cast UI
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(true);

        if (fishList.eligibleFish.Count==0) { yield break; }

        yield return StartCoroutine(hookTick(3));

        player.Screen.hookedSquare.gameObject.SetActive(true);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));

        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);

        if (Random.Range(0, 100) <= fishList.findingsChance)
        {
            Hooked(true, fishList.findings[Random.Range(0,fishList.findings.Count)]);
            yield break;
        }

        //minigame.parent.SetActive(true);

        //greenSize, pointerSpeedV, fillDiff
        //yield return StartCoroutine(minigame.Minigame(GreenSize, pointerSpeed, fillBarDiff, this, SelectFish()));
        AudioManager.instance.PlaySFX("spin");
        yield return StartCoroutine(minigame.Minigame(this, SelectFish())); 
    }
    IEnumerator hookTick(float time) 
    {
        while (true)
        {
            if (State == state.hooking) { yield break; }
            yield return new WaitForSeconds(time);
            Tick();
        }

        void Tick()
        {
            int a = RandomTick();
            if (a < stats.chanceToHook)
            {
                State = state.hooking;
                floatObject.GetComponent<FloatScript>().floatAnimation.FloatDown();
            }
        }
    }
    IEnumerator WaitForFishList()
    {
        while (fishList == null)
        {
            yield return null;
        }
        yield return true;
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode) 
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }

    public void ThrowFloat()
    {
        if (State == state.idle)
        {
            if (floatObject != null) { Destroy(floatObject); }
            floatObject = Instantiate(player.inventory.splawik.prefab, components.splawik.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));

            FloatScript fs = floatObject.GetComponent<FloatScript>();
            fs.Throw(player.cam.transform.forward);
            fs.floatAnimation.FloatUp();
            fs.rod = this;
        }
    }
}
