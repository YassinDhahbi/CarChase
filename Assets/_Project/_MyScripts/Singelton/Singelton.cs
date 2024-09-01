using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    public bool isDestroyble = false;

    protected void Awake()
    {
        if (isDestroyble)
        {
            SingltonDestroyble();
        }
        else
        {
            SingltonNotDestroyble();
        }
    }


    private void SingltonDestroyble()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this as T;
        }
    }

    private void SingltonNotDestroyble()
    {
        if (Instance == null)
        {
            transform.parent = null;
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}