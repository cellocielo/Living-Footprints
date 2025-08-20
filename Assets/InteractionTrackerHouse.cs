using UnityEngine;
using System.Collections.Generic;

public class InteractionTrackerHouse : MonoBehaviour
{
    public static InteractionTrackerHouse Instance;
    
    private HashSet<string> interactedObjects = new HashSet<string>();
    
    public Dictionary<string, string> allInteractiveObjects = new Dictionary<string, string>()
    {
        {"Garden Plants", "Water the Plants"},
        {"PS4", "Use the PS4"},
        {"Tree", "Check the Tree"},
        {"Rain Barrel", "Check Rain Barrel"},
        {"Pizza Box", "Recycle Pizza Box"},
        {"Car", "Check the Car"}
    };
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void MarkAsInteracted(string objectTag)
    {
        interactedObjects.Add(objectTag);
        Debug.Log($"Marked {objectTag} as interacted");
    }
    
    public bool HasInteracted(string objectTag)
    {
        return interactedObjects.Contains(objectTag);
    }
    
    public List<string> GetUninteractedObjects()
    {
        List<string> uninteracted = new List<string>();
        
        foreach (var kvp in allInteractiveObjects)
        {
            if (!interactedObjects.Contains(kvp.Key))
            {
                uninteracted.Add(kvp.Value);
            }
        }
        
        return uninteracted;
    }
    
    public void ResetInteractions()
    {
        interactedObjects.Clear();
    }
}