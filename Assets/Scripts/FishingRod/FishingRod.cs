using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public state State;
    public enum state
    {
        idle,cast,hooking,
    }

    public Player player;
    public int chanceToHook;

    public GameObject floatObject;
    public Transform whereToSpawnFloat;
    public GameObject floatPrefab;
    public FishingMinigame minigame;

    public FishList fishList;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (State == state.cast) { Hooked(false); return; }
            StartCoroutine(Throw());
        }
    }
    public int RandomTick()
    {
        return Random.Range(0, 100);
    }

    public void Hooked(bool caught)
    {
        minigame.parent.SetActive(false);
        State = state.idle;
        Destroy(floatObject);
        Debug.Log("Hooked");
        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);
        fishList = null;
        StopAllCoroutines();

        if (caught)
        {

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
        yield return StartCoroutine(minigame.Minigame(2, 1, 4, this));
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
