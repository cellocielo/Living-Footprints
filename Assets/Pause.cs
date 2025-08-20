using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void TogglePauseMenu()
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
