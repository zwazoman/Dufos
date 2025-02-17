using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSnap : MonoBehaviour
{
    private void Awake()
    {
        transform.position.SnapOnGrid();
        transform.position = Vector3.up * 0.5f;
    }
}

