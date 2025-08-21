using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClosePanel : MonoBehaviour
{
    public Button x;
    public GameObject panelToClose;
    public GameObject text;

    [Header("Task Completion")]
    public string taskType;

    void Start()
    {
        x.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame()
    {
        Debug.Log("closing panel");
        panelToClose.SetActive(false);
        x.gameObject.SetActive(false);
        text.SetActive(false);
        Time.timeScale = 1f;

        if (DigitalClock.Instance != null)
        {
            DigitalClock.Instance.TogglePause();
            Debug.Log("Clock toggled on UI close");
        }

        if (!string.IsNullOrEmpty(taskType))
        {
            SceneStateManager.CompleteTask(taskType);
            Debug.Log("Task Name: " + taskType);
            Debug.Log("Complete Task Method Called");
        }

        if (TimeBasedInteraction.shouldCompleteSecondLaundryTask)
        {
            SceneStateManager.CompleteTask("laundry_second");
            TimeBasedInteraction.shouldCompleteSecondLaundryTask = false;
            Debug.Log("Second laundry task completed on panel close!");
        }
    }
}