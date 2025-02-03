using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Tools
{
    public static Vector3[] AllFlatDirections = { Vector3.forward, Vector3.right, -Vector3.forward, -Vector3.right};

    public static Vector3Int SnapOnGrid(this Vector3 initialPos)
    {
        return new Vector3Int(Mathf.RoundToInt(initialPos.x), 0, Mathf.RoundToInt(initialPos.z));
    }

    public static T PickRandomInList<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    } 

    public static T pickRandomInArray<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

}
