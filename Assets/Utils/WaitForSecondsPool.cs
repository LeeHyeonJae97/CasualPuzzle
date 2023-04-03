using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsPool
{
    private static readonly Dictionary<float, WaitForSeconds> _pool = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds Get(float seconds)
    {
        if (!_pool.ContainsKey(seconds))
        {
            _pool.Add(seconds, new WaitForSeconds(seconds));
        }
        return _pool[seconds];
    }
}