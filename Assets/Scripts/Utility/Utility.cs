using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}