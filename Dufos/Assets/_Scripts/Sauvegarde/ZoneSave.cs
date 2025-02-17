using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSave : MonoBehaviour
{
    private bool _canSave = true;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canSave == true)
        {

        }
    }
}
