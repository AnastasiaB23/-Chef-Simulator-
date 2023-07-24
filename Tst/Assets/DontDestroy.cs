using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static bool instanced = false;
    private void Awake()
    {
        if (!instanced)
        {
            DontDestroyOnLoad(this);
            instanced = true;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
