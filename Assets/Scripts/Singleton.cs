using System;
using UnityEngine;

public class Singleton<T>
    where T : Singleton<T>, new()
{
    private static T? s_instance;

    public static T Instance => s_instance ??= new T();
}