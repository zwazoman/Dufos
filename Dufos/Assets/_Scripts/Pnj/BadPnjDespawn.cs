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
        switch (GameManager.Instance.ZoneName)
        {
            case "Zone 1":
                for (int i = 0; i < GameManager.Instance.Fighters.Count - 6; i++)
                {
                    if (GameManager.Instance.Fighters[i] != false)
                    {
                        _zone1BadPnj[i].SetActive(false);
                    }
                }
                break;
            case "Zone 2":
                for (int i = 3; i < GameManager.Instance.Fighters.Count - 3; i++)
                {
                    if (GameManager.Instance.Fighters[i] != false)
                    {
                        _zone2BadPnj[i - 3].SetActive(false);
                    }
                }
                break;
            case "Zone 3":
                for (int i = 6; i < GameManager.Instance.Fighters.Count; i++)
                {
                    if (GameManager.Instance.Fighters[i] != false)
                    {
                        _zone3BadPnj[i - 6].SetActive(false);
                    }
                }
                break;

            default:
                break;
        }
    }
}
