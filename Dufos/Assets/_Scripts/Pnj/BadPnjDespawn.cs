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
        if (GameManager.Instance.ZoneName == "Zone 1")
        {
            if (GameManager.Instance.Fighters[0] == true)
            {
                _zone1BadPnj[0].SetActive(false);
            }
            if (GameManager.Instance.Fighters[1] == true)
            {
                _zone1BadPnj[1].SetActive(false);
            }
            if (GameManager.Instance.Fighters[2] == true)
            {
                _zone1BadPnj[2].SetActive(false);
            }
        }
        if (GameManager.Instance.ZoneName == "Zone 2")
        {
            if (GameManager.Instance.Fighters[3] == true)
            {
                _zone2BadPnj[0].SetActive(false);
            }
            if (GameManager.Instance.Fighters[4] == true)
            {
                _zone2BadPnj[1].SetActive(false);
            }
            if (GameManager.Instance.Fighters[5] == true)
            {
                _zone2BadPnj[2].SetActive(false);
            }
        }
        if (GameManager.Instance.ZoneName == "Zone 3")
        {
            if (GameManager.Instance.Fighters[6] == true)
            {
                _zone3BadPnj[0].SetActive(false);
            }
            if (GameManager.Instance.Fighters[7] == true)
            {
                _zone3BadPnj[1].SetActive(false);
            }
            if (GameManager.Instance.Fighters[8] == true)
            {
                _zone3BadPnj[2].SetActive(false);
            }
        }
    }
}
