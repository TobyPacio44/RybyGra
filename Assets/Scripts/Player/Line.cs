using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Line : MonoBehaviour
{
    public LineRenderer line;
    public Transform rod;
    public Transform floatPos;

    private void Start()
    {
        line.positionCount = 2;
    }
    public void SetLine(Transform transform)
    {
        floatPos = transform;
    }
    private void Update()
    {
        line.SetPosition(0, rod.position);
        line.SetPosition(1, floatPos.position);
    }
}
