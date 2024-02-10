using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatAnimation : MonoBehaviour
{
    public void FloatUp()
    {
        GetComponent<Animation>().Play("floatUp");
    }

    public void FloatDown()
    {
        GetComponent<Animation>().Play("floatDown");
    }

    public void FloatCatch()
    {
        GetComponent<Animation>().Play("floatCatch");
    }
}
