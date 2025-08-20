using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class Checklist : MonoBehaviour 
{
    public GameObject panel;
    public Transform contentParent;
    
    private TextMeshProUGUI checklistText;

    void Start() {
        checklistText = contentParent.GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if (panel == null) {
            panel = GameObject.Find("Checklist Panel");
            panel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            TogglePanel();
        }
    }
    
    public void TogglePanel()
    {
        if (panel.activeSelf)
        {
            ClosePanel();
        }
        else
        {
            OpenPanel();
        }
    }
    
    private void UpdateChecklistContent()
    {
        List<string> uninteractedObjects = GetUninteractedObjectsForCurrentScene();
        
        // Use the TextMeshProUGUI component attached to contentParent (Content GameObject)
        if (checklistText != null)
        {
            if (uninteractedObjects.Count == 0)
            {
                checklistText.text = "<b><size=32>Remaining Tasks:</size></b>\n<size=26>All tasks completed!</size>";
            }
            else
            {
                checklistText.text = "<b><size=32>Remaining Tasks:</size></b>\n<size=26>";
                foreach (string task in uninteractedObjects)
                {
                    checklistText.text += "• " + task + "\n";
                }
                checklistText.text += "</size>";
            }
        }
    }
    
    private List<string> GetUninteractedObjectsForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "HouseScene")
        {
            if (InteractionTrackerHouse.Instance != null)
            {
                return InteractionTrackerHouse.Instance.GetUninteractedObjects();
            }
        }
        else if (currentSceneName == "ApartmentScene")
        {
            if (InteractionTrackerApartment.Instance != null)
            {
                return InteractionTrackerApartment.Instance.GetUninteractedObjects();
            }
        }
        return new List<string>();
    }
    
    public void OpenPanel()
    {
        UpdateChecklistContent();
        panel.SetActive(true);
    }
    
    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}