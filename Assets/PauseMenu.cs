using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void TogglePause()
    {
        bool isActive = pausePanel.activeSelf;
        pausePanel.SetActive(!isActive);
        if (isActive) {
            Time.timeScale = 1;
        }
        else {
            Time.timeScale = 0;
        }
    }
}
