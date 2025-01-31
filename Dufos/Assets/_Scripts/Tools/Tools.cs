using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Tools
{
    public static Vector3Int SnapOnGrid(this Vector3 initialPos)
    {
        return new Vector3Int(Mathf.RoundToInt(initialPos.x), 0, Mathf.RoundToInt(initialPos.z));
    }

    public static T PickRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    } 
}
