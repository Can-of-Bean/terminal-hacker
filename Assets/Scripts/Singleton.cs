using System;
using UnityEngine;

public class Singleton<T>
    where T : Singleton<T>, new()
{
    public static T Instance { get; private set; } = null!;
    
    public Singleton()
    {
        if (Instance != null)
            throw new InvalidOperationException("Cannot create duplicate instance of singleton behaviour");

        Instance = new T();
    }
}