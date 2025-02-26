using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            return;
        }

        instance = this;

    }

    public int FightsWon;
    public string ZoneName;
    public Vector3 PlayerPosition;
    public int WichFight;
    public List<bool> Fighters = new List<bool>() { false, false, false, false, false, false, false, false, false };

    public void NextScene()
    {
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
    }
}
