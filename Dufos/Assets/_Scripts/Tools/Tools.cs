using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public static class Tools
{
    public static Vector3[] AllFlatDirections = { Vector3.forward, Vector3.right, -Vector3.forward, -Vector3.right};

    public static Vector3Int SnapOnGrid(this Vector3 initialPos)
    {
        return new Vector3Int(Mathf.RoundToInt(initialPos.x), 0, Mathf.RoundToInt(initialPos.z));
    }

    public static T PickRandom<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }
    public static T PickRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static Vector3 FindClosest(this Vector3[] points, Vector3 origin)
    {
        if (points.Length == 0)
        {
            Debug.LogError("Array Is Empty");
            return Vector3.zero;
        }
        if (points.Length == 1)
        {
            return points[0];
        }

        Vector3 closest = points[0];

        foreach (Vector3 point in points)
        {
            Vector3 pointOffset = point - origin;
            Vector3 closestOffset = closest - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = point;
        }

        return closest;
    }

    public static Vector3 FindClosest(this List<Vector3> points, Vector3 origin)
    {
        if (points.Count == 0)
        {
            Debug.LogError("List Is Empty");
            return Vector3.zero;
        }
        if (points.Count == 1)
        {
            return points[0];
        }

        Vector3 closest = points[0];

        foreach (Vector3 point in points)
        {
            Vector3 pointOffset = point - origin;
            Vector3 closestOffset = closest - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = point;
        }

        return closest;
    }

    public static Transform FindClosest(this Transform[] transforms, Vector3 origin)
    {
        if (transforms.Length == 0)
        {
            Debug.LogError("List Is Empty");
            return null;
        }
        if (transforms.Length == 1)
        {
            return transforms[0];
        }

        Transform closest = transforms[0];

        foreach (Transform t in transforms)
        {
            Vector3 pointOffset = t.position - origin;
            Vector3 closestOffset = closest.position - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = t;
        }

        return closest;

    }

    public static Transform FindClosest(this List<Transform> transforms, Vector3 origin)
    {
        if (transforms.Count == 0)
        {
            Debug.LogError("List Is Empty");
            return null;
        }
        if (transforms.Count == 1)
        {
            return transforms[0];
        }

        Transform closest = transforms[0];

        foreach (Transform t in transforms)
        {
            Vector3 pointOffset = t.position - origin;
            Vector3 closestOffset = closest.position - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = t;
        }

        return closest;

    }

    public static GameObject FindClosest(this GameObject[] elements, Vector3 origin)
    {
        if (elements.Length == 0)
        {
            Debug.LogError("List Is Empty");
            return null;
        }
        if (elements.Length == 1)
        {
            return elements[0];
        }

        GameObject closest = elements[0];

        foreach (GameObject element in elements)
        {
            Vector3 pointOffset = element.transform.position - origin;
            Vector3 closestOffset = closest.transform.position - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = element;
        }

        return closest;

    }

    public static GameObject FindClosest(this List<GameObject> elements, Vector3 origin)
    {
        if (elements.Count == 0)
        {
            Debug.LogError("List Is Empty");
            return null;
        }
        if (elements.Count == 1)
        {
            return elements[0];
        }

        GameObject closest = elements[0];

        foreach (GameObject element in elements)
        {
            Vector3 pointOffset = element.transform.position - origin;
            Vector3 closestOffset = closest.transform.position - origin;

            if (pointOffset.sqrMagnitude < closestOffset.sqrMagnitude)
                closest = element;
        }

        return closest;

    }

    public static T FindClosest<T>(this List<T> elements, Vector3 origin) where T : Component
    {
        if (elements.Count == 0)
        {
            Debug.LogError("List Is Empty");
            return null;
        }
        if (elements.Count == 1)
        {
            return elements[0];
        }

        T closest = elements[0];

        float closestDistance = (closest.transform.position - origin).sqrMagnitude;

        foreach (T element in elements)
        {
                                                                   
            if ((element.transform.position - origin).sqrMagnitude < closestDistance)
            {
                closest = element;
                closestDistance = (closest.transform.position - origin).sqrMagnitude;
            }
        }

        return closest;
    }
}
