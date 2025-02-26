using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFight : MonoBehaviour
{
    public void ChangeScene()
    {
        GameManager.Instance.NextZone();
    }

    public void SimpleChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
