using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        SavedDataCenter.Instance.TryLoadSave();
        FightsWon = SavedDataCenter.Instance.Data.ClearedCampsCount;
    }

    public int FightsWon { get; set; }
    public string ZoneName { get; set; }
    public Vector3 PlayerPosition { get; set; }
    public int WhichFight { get; set; }
    public List<bool> Fighters = new List<bool>() { false, false, false, false, false, false, false, false, false };

    public void NextZone()
    {
        switch (FightsWon)
        {
            case 1:
                ZoneName = "Zone 1";
                ChangeZone(ZoneName);
                break;
            case 3:
                PlayerPosition = Vector3.zero;
                ZoneName = "Zone 2";
                ChangeZone(ZoneName);
                break;
            case 6:
                PlayerPosition = Vector3.zero;
                ZoneName = "Zone 3";
                ChangeZone(ZoneName);
                break;
            case 9:
                SceneManager.LoadScene("VictoryScene");
                break;
            default:
                ChangeZone(ZoneName);
                break;
        }
    }

    public void ChangeZone(string zone)
    {
        switch (zone)
        {
            case "Zone 1":
                Fighters[WhichFight] = true;
                SceneManager.LoadScene(ZoneName);
                SaveMap(ZoneName);
                break;
            case "Zone 2":
                Fighters[WhichFight] = true;
                SceneManager.LoadScene(ZoneName);
                SaveMap(ZoneName);
                break;
            case "Zone 3":
                Fighters[WhichFight] = true;
                SceneManager.LoadScene(ZoneName);
                SaveMap(ZoneName);
                break;
        }
    }

    public void SaveMap(string zoneName)
    {
        SavedDataCenter.Instance.Data.CurrentMap = zoneName;
        SavedDataCenter.Instance.Save();
    }
}
