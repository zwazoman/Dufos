using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public int FightsWon;
    public string ZoneName;
    public Vector3 PlayerPosition;
    public int WichFight;
    public List<bool> Fighters = new List<bool>() { false, false, false, false, false, false, false, false, false };

    public void NextZone()
    {
        if (FightsWon < 3)
        {
            ZoneName = "Zone 1";
            Fighters[WichFight] = true;
            SceneManager.LoadScene("Zone 1");
            return;
        }
        if (FightsWon >= 3 && FightsWon < 6)
        {
            ZoneName = "Zone 2";
            Fighters[WichFight] = true;
            SceneManager.LoadScene("Zone 2");
            return;
        }
        if (FightsWon >= 6 && FightsWon < 9)
        {
            ZoneName = "Zone 3";
            Fighters[WichFight] = true;
            SceneManager.LoadScene("Zone 3");
            return;
        }
        else
        {
            SceneManager.LoadScene("Win");
            return;
        }
        /*
        switch (FightsWon)
        {
            case 3:
                ZoneName = "Zone2";
                SceneManager.LoadScene("Zone2");
                break;
            case 6:
                ZoneName = "Zone3";
                SceneManager.LoadScene("Zone3");
                break;
            case 9:
                SceneManager.LoadScene("Win");
                break;
        }
        */
    }
}
