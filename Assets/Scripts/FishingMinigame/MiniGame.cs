using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject UI;

    public Transform topPivot;
    public Transform botPivot;
    public Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [Header("How long to wait for new position")]
    public float timerMultiplicator = 3f;

    float fishSpeed;
    [Header("Szybkoœæ ryby 1-najszybsza")]
    public float smoothMotion = 1f;

    public Transform hook;
    float hookPosition;
    public float hookSize = 0.1f;
    float hookProgress;
    public float hookPullVelocity;
    public float hookPullPower = 0.01f;
    public float hookGravityPower = 0.005f;

    [Header("Góra i dó³ progress")]
    public float hookPower = 5f;
    public float hookDegrade = 0.1f;

    public Image hookRenderer;
    public Image fishRenderer;
    public Transform progressContainer;

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
