using System;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour 
    where T : SingletonBehaviour<T>
{
    public static T Instance { get; private set; } = null!;
    
    public SingletonBehaviour()
    {
        if (Instance != null)
            throw new InvalidOperationException("Cannot create duplicate instance of singleton behaviour");

        Instance = (T)this;
    }
}