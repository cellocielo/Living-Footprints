using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneStateManager : MonoBehaviour
{
    public static bool showFridgeUI = false;
    public static bool showLaundryUI = false;
    public static bool showPlantsUI = false;
    public static bool showPanUI = false;
    public static bool showTreeUI = false;
    public static bool showPillUI = false;
    public static bool showEWasteUI = false;
    public static bool showSolarPanelUI = false;
    public static bool showPizzaBoxUI = false;
    public static bool showRainBarrelUI = false;
    public static bool showPS4UI = false;
    public static bool showCarUI = false;
    public static bool showJamUI = false;
    public static bool showNativePlantUI = false;

    public static Vector3 savedPlayerPosition;
    public static bool hasStoredPosition = false;
    
    // store camera position
    public static Vector3 savedCameraPosition;
    public static bool hasSavedCameraPosition = false;
    
    private static HashSet<string> completedTasks = new HashSet<string>();
    
    private static HashSet<string> apartmentTasks = new HashSet<string>()
    {
        "fridge", "laundry", "ewaste", "pill", "pan", "plants", "solarpanel, laundry_second"
    };
    
    private static HashSet<string> houseTasks = new HashSet<string>()
    {
        "tree", "pizzabox", "rainbarrel", "ps4", "car", "jam", "nativeplant"
    };
    
    private static Dictionary<string, string> taskNames = new Dictionary<string, string>()
    {
        {"fridge", "Diet Task Completed!"},
        {"laundry", "Energy Demand Task Completed!"},
        {"laundry_second", "Check Laundry at Different Energy Demand Completed!"},
        {"plants", "Roof Garden Task Completed!"},
        {"pan", "Cleaning Pan Task Completed!"},
        {"tree", "Check Tree Task Completed!"},
        {"pill", "Pharmaceuticals Task Completed!"},
        {"ewaste", "E-Waste Recycling Task Completed!"},
        {"solarpanel", "Solar Energy Task Completed!"},
        {"pizzabox", "Discard Trash Task Completed!"},
        {"rainbarrel", "Water Conservation Task Completed!"},
        {"ps4", "Energy Saving Task Completed!"},
        {"car", "Transportation Task Completed!"},
        {"jam", "Material Preservation Task Completed!"},
        {"nativeplant", "Native Plants Task Completed!"},
    };

    private void Start()
    {
        Debug.Log($"Game started. Completed tasks count: {completedTasks.Count}");
        if (hasStoredPosition)
        {
            Debug.Log($"SceneStateManager: Has stored position: {savedPlayerPosition}");
        }
    }

    public static void StorePlayerPosition()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            savedPlayerPosition = player.transform.position;
            hasStoredPosition = true;
            Debug.Log($"Stored player position: {savedPlayerPosition}");
            
            if (Camera.main != null)
            {
                savedCameraPosition = Camera.main.transform.position;
                hasSavedCameraPosition = true;
                Debug.Log($"Stored camera position: {savedCameraPosition}");
            }
        }
        else
        {
            Debug.LogError("Player not found when trying to store position!");
        }
    }
    
    public static void CompleteTask(string taskType)
    {
        if (string.IsNullOrEmpty(taskType))
        {
            Debug.LogWarning("Task type is null or empty!");
            return;
        }
        
        taskType = taskType.ToLower();
        
        if (completedTasks.Contains(taskType))
        {
            Debug.Log($"Task {taskType} was already completed.");
            return;
        }
        
        completedTasks.Add(taskType);
        
        string displayName = taskNames.ContainsKey(taskType) ? taskNames[taskType] : "Task Completed!";
        
        TaskCompletionPopup.ShowTaskCompletionWithType(displayName, taskType);
        
        Debug.Log($"Task completed: {taskType} - {displayName}");
        
        if (IsApartmentCompleted())
        {
            Debug.Log("All apartment tasks completed!");
        }
        
        if (IsHouseCompleted())
        {
            Debug.Log("All house tasks completed!");
        }
    }
    
    public static bool IsTaskCompleted(string taskType)
    {
        if (string.IsNullOrEmpty(taskType)) return false;
        return completedTasks.Contains(taskType.ToLower());
    }
    
    public static bool IsApartmentCompleted()
    {
        foreach (string task in apartmentTasks)
        {
            if (!completedTasks.Contains(task))
            {
                return false;
            }
        }
        return true;
    }
    
    public static int GetCompletedApartmentTaskCount()
    {
        int count = 0;
        foreach (string task in apartmentTasks)
        {
            if (completedTasks.Contains(task))
            {
                count++;
            }
        }
        return count;
    }
    
    public static int GetTotalApartmentTaskCount()
    {
        return apartmentTasks.Count;
    }
    
    public static bool IsHouseCompleted()
    {
        foreach (string task in houseTasks)
        {
            if (!completedTasks.Contains(task))
            {
                return false;
            }
        }
        return true;
    }
    
    public static int GetCompletedHouseTaskCount()
    {
        int count = 0;
        foreach (string task in houseTasks)
        {
            if (completedTasks.Contains(task))
            {
                count++;
            }
        }
        return count;
    }
    
    public static int GetTotalHouseTaskCount()
    {
        return houseTasks.Count;
    }
    
    public static int GetCompletedTaskCount()
    {
        return completedTasks.Count;
    }
    
    public static float GetTaskCompletionPercentage()
    {
        int totalTasks = taskNames.Count;
        if (totalTasks == 0) return 0f;
        return (float)completedTasks.Count / totalTasks * 100f;
    }

    public static void LoadAppropriateScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == "PhantomLoadScene" || currentScene == "TreeScene" ||
            currentScene == "RainBarrelScene" ||
            currentScene == "PizzaBoxScene" || currentScene == "CarScene" || 
            currentScene == "Jam" || currentScene == "NativePlantScene")
        {
            SceneManager.LoadScene("HouseScene");
        }
        else
        {
            if (DigitalClock.Instance != null)
            {
                DigitalClock.Instance.gameObject.SetActive(true);
            }
            SceneManager.LoadScene("ApartmentScene");
        }
    }

    public static void CloseAndReturnToMain()
    {
        LoadAppropriateScene();
    }

    public static void ResetFlags()
    {
        showPlantsUI = false;
        showFridgeUI = false;
        showPS4UI = false;
        showPillUI = false;
        showLaundryUI = false;
        showTreeUI = false;
        showRainBarrelUI = false;
        showSolarPanelUI = false;
        showPizzaBoxUI = false;
        showEWasteUI = false;
        showPanUI = false;
        showCarUI = false;
        showJamUI = false;
        showNativePlantUI = false;
    }
    
    public static void ResetAllProgress()
    {
        completedTasks.Clear();
        Debug.Log("All progress has been reset.");
    }
}