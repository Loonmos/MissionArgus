using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoy : MonoBehaviour
{
    public static DontDestoy Instance;

    private void Awake()
    {
        Check();
    }

    private void Check()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}
