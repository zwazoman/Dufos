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

    public int FightsWon;
    public string ZoneName;
    public Vector3 PlayerPosition;
    public int WhichFight;
    public List<bool> Fighters = new List<bool>() { false, false, false, false, false, false, false, false, false };

    public void NextZone()
    {

        if (FightsWon < 3)
        {
            ZoneName = "Zone 1";
            Fighters[WhichFight] = true;
            SceneManager.LoadScene("Zone 1");
            SaveMap(ZoneName);
            return;
        }
        if (FightsWon >= 3 && FightsWon < 6)
        {
            if (ZoneName != "Zone 2")
            {
                PlayerPosition = Vector3.zero;
            }
            ZoneName = "Zone 2";
            Fighters[WhichFight] = true;
            SceneManager.LoadScene("Zone 2");
            SaveMap(ZoneName);
            return;
        }
        if (FightsWon >= 6 && FightsWon < 9)
        {
            if (ZoneName != "Zone 3")
            {
                PlayerPosition = Vector3.zero;
            }
            ZoneName = "Zone 3";
            Fighters[WhichFight] = true;
            SceneManager.LoadScene("Zone 3");
            SaveMap(ZoneName);
            return;
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
            return;
        }
    }

    public void SaveMap(string zoneName)
    {
        SavedDataCenter.Instance.Data.CurrentMap = zoneName;
        SavedDataCenter.Instance.Save();
    }
}
