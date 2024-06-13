using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    public GameObject parent;

    [Header("greenSize - from 1 to 10")]
    public float GreenSize;
    [Header("pointerSpeed - from 1 to 10(more extreme after)")]
    public float pointerSpeed;
    [Header("fillBar - from 1 to 4(more extreme after)")]
    public float fillBarDiff;

    public bool won;
    public GameObject green;
    public RectTransform rect;
    public RectTransform blueRect;

    public side Side;
    public enum side
    {
        right,left
    }

    public bool onGreen;
    private Vector3 velocity = Vector3.zero;
    public float dampingFactor = 0.9f; // Damping factor to reduce velocity over time
    private void Start()
    {
    }
    public IEnumerator Minigame(float greenSize, float pointerSpeedV, float fillDiff, FishingRod rod, FishObject fish)
    {
        GreenSize = greenSize;
        pointerSpeed = pointerSpeedV;
        fillBarDiff = fillDiff;
        green.transform.localScale = new Vector3(GreenSize / 10, 1, 1);
        rect.transform.localPosition = new Vector3(-50, rect.transform.localPosition.y, rect.transform.localPosition.z);
        blueRect.transform.localScale = new Vector3(0, 1, 1);

        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(Progress());

        if (won)
        {
            //pass the bool if caught or not
            rod.Hooked(true, fish);
        }
        else
        {
            //pass the bool if caught or not
            rod.Hooked(false, null);
        }
    }
    private void Update()
    {
        if (rect.transform.localPosition.x < -49.9)
        {
            Side = side.right;    
        }
        if (rect.transform.localPosition.x > 49.9)
        {
            Side = side.left;
        }

    }


    private void FixedUpdate()
    {
        float acceleration = 0.0f;

        if (Side == side.right)
        {
            acceleration = pointerSpeed * 40 * Time.deltaTime;
        }
        if (Side == side.left)
        {
            acceleration = -pointerSpeed * 40 * Time.deltaTime;
        }

        velocity.x += acceleration;
        velocity.x *= dampingFactor; // Apply damping

        Vector3 newPosition = rect.localPosition + velocity * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -50f, 50f);
        rect.localPosition = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == green)
        {
            onGreen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == green)
        {
            onGreen = false;
        }
    }

    public IEnumerator Progress()
    {
        while (blueRect.transform.localScale.x < 1 && blueRect.transform.localScale.x >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(WaitForKeyDown(KeyCode.Space));

            if (onGreen)
            {
                blueRect.transform.localScale += new Vector3(1 / fillBarDiff, 0, 0);
                Debug.Log("+");
            }
            else
            {

                blueRect.transform.localScale -= new Vector3(1 / fillBarDiff, 0, 0);
                Debug.Log("-");

            }

            if (Random.Range(0, 10) > 5)
            {
            rect.transform.localPosition = new Vector3(-50, rect.transform.localPosition.y, rect.transform.localPosition.z);
            }
            else
            {
            rect.transform.localPosition = new Vector3(50, rect.transform.localPosition.y, rect.transform.localPosition.z);
            }

        }
        if (blueRect.transform.localScale.x <= 0) { won = false; yield return false; }
        else {  won = true; yield return true; }     
    }
    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }
}
