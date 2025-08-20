using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClosePanel3Graphs : MonoBehaviour 
{

    public Button x;
    public GameObject panelToClose;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    
    [Header("Task Completion")]
    public string taskType; // Set this in the Inspector for each panel

    void Start() {
        x.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame() {
        Debug.Log("closing panel");
        panelToClose.SetActive(false);
        x.gameObject.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        Time.timeScale = 1f;
        
        if (DigitalClock.Instance != null) {
            DigitalClock.Instance.TogglePause();
            Debug.Log("Clock toggled on UI close");
        }
        
        // Show task completion popup after closing the info panel
        if (!string.IsNullOrEmpty(taskType)) {
            SceneStateManager.CompleteTask(taskType);
            Debug.Log("Complete Task Method Called");
        }
    }
}