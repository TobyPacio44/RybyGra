using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public int chanceToHook;
    public int power;

    [Header("greenSize - from 1 to 10")]
    public float GreenSize;
    [Header("pointerSpeed - from 1 to 10(more extreme after)")]
    public float pointerSpeed;
    [Header("fillBar - from 1 to 4(more extreme after)")]
    public float fillBarDiff;

    [HideInInspector]
    public state State;
    public enum state
    {
        idle,cast,hooking,
    }

    [HideInInspector] public GameObject floatObject;
    public Transform whereToSpawnFloat;
    public GameObject floatPrefab;
    public FishingMinigame minigame;
    public Player player;

    [HideInInspector] public FishList fishList;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (State == state.cast) { Hooked(false, null); return; }
            StartCoroutine(Throw());
        }
    }
    public int RandomTick()
    {
        return Random.Range(0, 100);
    }

    public FishObject SelectFish()
    {
        List<FishObject> eligibleFish = new List<FishObject>();

        foreach (FishObject fish in fishList.list)
        {
            if (fish.weight <= power)
            {
                eligibleFish.Add(fish);
            }
        }

        if (eligibleFish.Count > 0)
        {
            FishObject selected = eligibleFish[Random.Range(0, eligibleFish.Count)];
            return selected;
        }
        else
        {
            return null;
        }
    }
    public void Hooked(bool caught, ItemObject fish)
    {
        minigame.parent.SetActive(false);
        State = state.idle;
        Destroy(floatObject);
        Debug.Log("Hooked");
        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);
        fishList = null;
        StopAllCoroutines();

        if (caught && fish != null)
        {
            player.inventory.fishes.Add(fish);
        }
    }

    IEnumerator Throw()
    {
        Debug.Log("Throw");

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
        Debug.Log("Cast");
        //Cast UI
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(true);

        yield return StartCoroutine(hookTick(3));

        player.Screen.hookedSquare.gameObject.SetActive(true);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));

        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);

        minigame.parent.SetActive(true);

        //greenSize, pointerSpeedV, fillDiff
        yield return StartCoroutine(minigame.Minigame(GreenSize, pointerSpeed, fillBarDiff, this, SelectFish())); 
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
            if (a < chanceToHook)
            {
                State = state.hooking;
                floatObject.GetComponent<FloatScript>().floatAnimation.FloatDown();
            }
        }
    }
    IEnumerator WaitForFishList()
    {
        Debug.Log("WaitForFishList");
        while (fishList == null)
        {
            yield return null;
        }
        Debug.Log("WaitForFishList Finished");

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
            floatObject = Instantiate(floatPrefab, whereToSpawnFloat.position, Quaternion.Euler(new Vector3(0, 0, -90)));

            FloatScript fs = floatObject.GetComponent<FloatScript>();
            fs.Throw(player.cam.transform.forward);
            fs.floatAnimation.FloatUp();
            fs.rod = this;
        }
    }
}
