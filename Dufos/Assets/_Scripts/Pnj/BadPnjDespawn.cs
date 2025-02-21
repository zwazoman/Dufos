using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPnjDespawn : MonoBehaviour
{
    [Header("Zone1 Bad Pnj")]
    [SerializeField] private List<GameObject> _zone1BadPnj = new List<GameObject>();

    [Header("Zone2 Bad Pnj")]
    [SerializeField] private List<GameObject> _zone2BadPnj = new List<GameObject>();

    [Header("Zone3 Bad Pnj")]
    [SerializeField] private List<GameObject> _zone3BadPnj = new List<GameObject>();


    private void Awake()
    {
        if (GameManager.instance.ZoneName == "Zone 1")
        {
            if (GameManager.instance.Fighters[0] == true)
            {
                _zone1BadPnj[0].SetActive(false);
            }
            if (GameManager.instance.Fighters[1] == true)
            {
                _zone1BadPnj[1].SetActive(false);
            }
            if (GameManager.instance.Fighters[2] == true)
            {
                _zone1BadPnj[2].SetActive(false);
            }
        }
        if (GameManager.instance.ZoneName == "Zone 2")
        {
            if (GameManager.instance.Fighters[3] == true)
            {
                _zone2BadPnj[0].SetActive(false);
            }
            if (GameManager.instance.Fighters[4] == true)
            {
                _zone2BadPnj[1].SetActive(false);
            }
            if (GameManager.instance.Fighters[5] == true)
            {
                _zone2BadPnj[2].SetActive(false);
            }
        }
        if (GameManager.instance.ZoneName == "Zone 3")
        {
            if (GameManager.instance.Fighters[6] == true)
            {
                _zone3BadPnj[0].SetActive(false);
            }
            if (GameManager.instance.Fighters[7] == true)
            {
                _zone3BadPnj[1].SetActive(false);
            }
            if (GameManager.instance.Fighters[8] == true)
            {
                _zone3BadPnj[2].SetActive(false);
            }
        }
        else
        {
            Debug.Log("HUH ? WHAT DO YOU MEAN ?");
        }
    }
}
