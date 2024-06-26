using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{

    [Header("Ile ryba czeka na ruch 3-czeka 1-nie czeka")]
    [Range(3, 1)]
    public float timerMultiplicator = 3f;
    [Header("2-wolna 0.1-szybka")]
    [Range(2, 0.1f)]
    public float smoothMotion = 1f;

    [Header("Wielkoœæ haczyka 0.1+")]
    public float hookSize = 0.1f;

    [Header("Góra i dó³ progress")]
    public float hookPower = 5f;
    public float hookDegrade = 0.1f;

    [Header("Reszta")]
    public GameObject UI;
    public Transform hook;
    public Transform topPivot;
    public Transform botPivot;
    public Transform fish;
    public Image hookRenderer;
    public Image fishRenderer;
    public Transform progressContainer;

    float hookPullPower = 0.04f;
    float hookGravityPower = 0.01f;
    float hookProgress;
    float hookPosition;
    float fishTimer;
    float fishSpeed;
    float fishPosition;
    float fishDestination;
    float hookPullVelocity;

    public bool pause;
    public bool won;

    public IEnumerator Minigame(FishingRod rod, FishObject fish)
    {
        UI.SetActive(true);
        fishRenderer.sprite = fish.sprite;
        won = false;
        pause = false;
        hookProgress = 0;
        Resize();

        while (!won)
        {
            yield return null;
        }

        rod.Hooked(true, fish);
        UI.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (pause) { return; }

        Fish();
        Hook();
        ProgressCheck();
    }

    void Resize()
    {
        float newScale = 1 + hookSize * 8 - 0.8f;
        hookRenderer.transform.localScale = new Vector3(hookRenderer.transform.localScale.x, newScale, hookRenderer.transform.localScale.z);
    }
    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        if (hook.position == botPivot.position || hook.position == botPivot.position)
        {
            hookPullVelocity = 0;
        }

        hookPullVelocity = Mathf.Clamp(hookPullVelocity, -0.008f, 0.008f);
        hookPosition += hookPullVelocity;
        hookPosition = Mathf.Clamp(hookPosition, hookSize/2, 1-hookSize/2);
        hook.position = Vector3.Lerp(botPivot.position, topPivot.position, hookPosition);
    }

    void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = Random.value * timerMultiplicator;

            fishDestination = Random.value;
            fishDestination = Mathf.Clamp(fishDestination, fishPosition - 0.4f, fishPosition + 0.4f);
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(botPivot.position, topPivot.position, fishPosition);
    }

    void ProgressCheck()
    {
        Vector3 ls = progressContainer.localScale;
        ls.y = hookProgress;
        progressContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if(min<fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookDegrade * Time.deltaTime;
        }

        if(hookProgress >= 1f)
        {
            won = true;
            pause = true;
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }
}
