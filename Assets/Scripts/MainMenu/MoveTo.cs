using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public float seconds;
    private float timer;
    public Vector3 Point;
    private Vector3 Difference;
    public Vector3 start;
    private float percent;
    void Start()
    {
        start = transform.position;
        Difference = Point - start;
    }

    void Update()
    {
        if (timer <= seconds)
        {
            timer += Time.deltaTime;
            percent = timer / seconds;
            transform.position = start + Difference * percent;
        }
    }
}
