using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClosePanel2Graphs : MonoBehaviour {

    public Button x;
    public GameObject panelToClose;
    public GameObject text1;
    public GameObject text2;
    
    [Header("Task Completion")]
    public string taskType;

    void Start() {
        x.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame() {
        Debug.Log("closing panel");
        panelToClose.SetActive(false);
        x.gameObject.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        Time.timeScale = 1f;
        
        if (DigitalClock.Instance != null) {
            DigitalClock.Instance.TogglePause();
            Debug.Log("Clock toggled on UI close");
        }
        
        if (!string.IsNullOrEmpty(taskType)) {
            SceneStateManager.CompleteTask(taskType);
            Debug.Log("Complete Task Method Called");
        }
    }
}