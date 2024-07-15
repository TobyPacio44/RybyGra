using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public static FollowMouse instance;
    public TextMeshProUGUI text;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
