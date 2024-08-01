using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton staticSingleton;

    public static Singleton Instance()
    {
        if (staticSingleton == null)
        {
            staticSingleton = new Singleton();
        }

        return staticSingleton;
    }
}