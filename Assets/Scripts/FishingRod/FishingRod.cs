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
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (State == state.cast) { Hooked(false); return; }
            StartCoroutine(Cast());
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
        StopAllCoroutines();

        if (caught)
        {

        }
    }

    IEnumerator Cast()
    {
        if (State == state.hooking) { yield break; }
        GetComponent<Animation>().Play("RodAnimation");
        yield return new WaitForSeconds(0.45f);
        ThrowFloat();
        State = state.cast;
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
            }
        }
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
            floatObject.GetComponent<floatThrowing>().Throw(player.cam.transform.forward);
            floatObject.GetComponent<FloatHelper>().floatAnimation.FloatUp();
        }
    }
}
