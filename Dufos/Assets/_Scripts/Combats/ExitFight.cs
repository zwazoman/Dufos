using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFight : MonoBehaviour
{
    public void ChangeScene()
    {
        GameManager.instance.NextZone();
    }

    public void SimpleChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
