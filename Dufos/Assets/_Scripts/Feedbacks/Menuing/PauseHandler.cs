using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _pausePanel;

    public void onPauseClicked()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void onPauseUnclicked()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
