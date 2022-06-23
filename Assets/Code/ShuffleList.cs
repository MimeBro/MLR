
using System;
using System.Collections.Generic;


public static class ShuffleList
{
    private static Random rng = new Random();

    /// <summary>
    /// Shuffles any list, based on Fisher–Yates shuffle.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}