using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour 
{
    public TextMeshProUGUI clockText;
    public float timeMultiplier = 1000f;
    private float timeElapsed = 0f;
    public Button closeLaundry;
    
    private bool isPaused = false;
    public static DigitalClock Instance;
    
    private const string TIME_ELAPSED_KEY = "DigitalClockTimeElapsed";
    
    void Awake() 
    {
        timeElapsed = PlayerPrefs.GetFloat(TIME_ELAPSED_KEY, 0f);
        Instance = this;
    }
    
    void Update() 
    {
        if (!isPaused) 
        {
            timeElapsed += Time.deltaTime * timeMultiplier;
        }
        
        int hours = Mathf.FloorToInt(timeElapsed / 3600) % 24;
        int minutes = Mathf.FloorToInt((timeElapsed % 3600) / 60);
        int displayHour = (hours % 12 == 0) ? 12 : hours % 12;
        string sign = (hours >= 12) ? "PM" : "AM";
        
        if (clockText != null) 
        {
            string timeString = string.Format("{0:D2}:{1:D2} {2}", displayHour, minutes, sign);
            clockText.text = timeString;
        }
    }
    
    // Call this method before changing scenes
    public void SaveTimeBeforeSceneChange()
    {
        PlayerPrefs.SetFloat(TIME_ELAPSED_KEY, timeElapsed);
        PlayerPrefs.Save();
    }
    
    public void OnInteractionHappened()
    {
        Debug.Log("DigitalClock received interaction notification");
        TogglePause();
        
        if (clockText != null)
        {
            clockText.gameObject.SetActive(false);
        }
    }
    
    public void TogglePause() 
    {
        isPaused = !isPaused;
        Debug.Log("Time Toggled: " + (isPaused ? "Paused" : "Running"));
    }
    
    public int GetCurrentHour() 
    {
        return Mathf.FloorToInt(timeElapsed / 3600) % 24;
    }
    
    public int GetCurrentMinute() 
    {
        return Mathf.FloorToInt((timeElapsed % 3600) / 60);
    }
    
    public void SetClockText(TextMeshProUGUI newText) 
    {
        clockText = newText;
    }
}