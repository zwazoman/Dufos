using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderBehaviour : MonoBehaviour
{
    private void Awake()
    {
        SavedDataCenter.Instance.TryLoadSave();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLastZone()
    {
        string zone = "Zone 1";
        if(SavedDataCenter.Instance.Data.CurrentMap != null && SavedDataCenter.Instance.Data.CurrentMap != "")
        {
            zone = SavedDataCenter.Instance.Data.CurrentMap;
        }
        SceneManager.LoadScene(zone);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
