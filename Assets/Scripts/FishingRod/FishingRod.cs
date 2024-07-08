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
            if (!canFish) { return; }
            if (holdingFish) { return; }
            if (player.inventory.fishesCapacity <= player.inventory.fishes.Count) { return; };
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
                        case 0: selected.weight = Random.Range(0.1f, 0.5f  ); break;
                        case 1: selected.weight = Random.Range(0.6f, 1.5f  ); break;
                        case 2: selected.weight = Random.Range(1.6f, 3f    ); break;
                        case 3: selected.weight = Random.Range(3.1f, 10f   ); break;
                        case 4: selected.weight = Random.Range(10.1f,50f   ); break;
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
    public void Hooked(bool caught, FishObject fish)
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
        fishList.StopAllCoroutines();
        fishList = null;
        StopAllCoroutines();

        if (caught && fish != null)
        {
            player.accept.gameObject.SetActive(true);
            player.accept.SetAcceptUI(fish.sprite, fish.name, fish.price, fish.weight);
            holdingFish = true;
            var instantiate = Instantiate(fish.prefab, fishSpawn.transform);
            StartCoroutine(takeFish(fish, instantiate));

            player.inventory.TakeOneBait();
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
    IEnumerator Throw()
    {

        if (State == state.hooking) { yield break; }
        GetComponent<Animation>().Play("RodAnimation");
        yield return new WaitForSeconds(0.45f);
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
