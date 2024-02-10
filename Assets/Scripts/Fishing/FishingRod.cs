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

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (State == state.cast) { Hooked(); return; }
            StartCoroutine(Cast());
        }
    }
    public int RandomTick()
    {
        return Random.Range(0, 100);
    }

    public void Hooked()
    {
        State = state.idle;
        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);
        Destroy(floatObject);
        Debug.Log("Hooked");
        StopAllCoroutines();
    }

    IEnumerator Cast()
    {
        if (State == state.hooking) { yield break; }
        ThrowFloat();
        State = state.cast;
        //Cast UI
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(true);

        yield return StartCoroutine(hookTick(3));

        player.Screen.hookedSquare.gameObject.SetActive(true);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));

        Hooked();
    }
    IEnumerator hookTick(float time) 
    {
        while (true)
        {
            if (State == state.hooking) { yield break; }
            Debug.Log("hookTick");
            yield return new WaitForSeconds(time);
            Tick();
        }

        void Tick()
        {
            int a = RandomTick();
            Debug.Log(a);
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
