using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Player player;
    public bool hooked;
    public int chanceToHook;

    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {            
            StartCoroutine(Cast());
        }
    }
    public int RandomTick()
    {
        return Random.Range(0, 100);
    }

    public void Hooked()
    {
        hooked = false;
        player.Screen.hookedSquare.gameObject.SetActive(false);
        player.Screen.hookedSquare.transform.parent.gameObject.SetActive(false);
        Debug.Log("Hooked");        
    }

    IEnumerator Cast()
    {
        if (hooked) { yield break; }

        Debug.Log("Casted");
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
            if (hooked) { yield break; }
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
                hooked = true;
            }
        }
    }
    IEnumerator WaitForKeyDown(KeyCode keyCode) 
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }
}
