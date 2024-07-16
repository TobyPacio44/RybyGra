using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public int difficulty;
    [Header("Ile ryba czeka na ruch 3-czeka 1-nie czeka")]
    [Range(4, 1)]
    public float timerMultiplicator = 4f;

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

    public float hookPullPower = 0.04f;
    public float hookGravityPower = 0.01f;
    public float smoothMotion = 1f;
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
        difficulty = fish.tier;
        AudioManager.instance.sfxSource.mute = true;
        UI.SetActive(true);
        fishRenderer.sprite = fish.sprite;
        won = false;
        pause = false;
        hookProgress = 0;
        DifficultyHandler();
        Resize();

        while (!won)
        {
            yield return null;
        }
        AudioManager.instance.sfxSource.mute = false;
        AudioManager.instance.PlaySFX("legato");
        rod.Hooked(true, fish);
        UI.SetActive(false);
    }
    public void DifficultyHandler()
    {
        switch(difficulty)
        {
            case 0:
                timerMultiplicator = 4;
                hookSize = 0.35f;
                hookDegrade = 0.1f;
                break;
            case 1:
                timerMultiplicator = 3;
                hookSize = 0.3f;
                hookDegrade = 0.2f;
                break;
            case 2:
                timerMultiplicator = 2;
                hookSize = 0.25f;
                hookDegrade = 0.3f;
                break;
            case 3:
                timerMultiplicator = 1;
                hookSize = 0.2f;
                hookDegrade = 0.4f;
                break;
            case 4:
                timerMultiplicator = 0.8f;
                hookSize = 0.18f;
                hookDegrade = 0.4f;
                break;
        }
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
            AudioManager.instance.sfxSource.mute = false;
        }
        else
        {
            AudioManager.instance.sfxSource.mute = true;
        }


        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        if (hook.position == botPivot.position || hook.position == botPivot.position)
        {
            hookPullVelocity = 0;
        }

        hookPullVelocity = Mathf.Clamp(hookPullVelocity, -0.008f, 0.01f);
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
