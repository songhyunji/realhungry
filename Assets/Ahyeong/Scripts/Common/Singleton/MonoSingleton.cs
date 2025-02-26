﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    T prefab = Resources.Load<T>("Prefabs/Singleton/" + typeof(T).ToString());
                    if (prefab != null)
                    {
                        Debug.Log("Prefab Singleton Created");
                        _instance = Instantiate(prefab) as T;
                        _instance.name = typeof(T).ToString();
                    }
                    else
                    {
                        Debug.Log("New Singleton Created");
                        _instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }
                }
            }

            return _instance;
        }
    }

    protected static T _instance = null;

    public void Echo() { }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
    }
}