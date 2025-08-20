using UnityEngine;
using System.Collections.Generic;

public class InteractionTrackerApartment : MonoBehaviour
{
    public static InteractionTrackerApartment Instance;
    
    private HashSet<string> interactedObjects = new HashSet<string>();
    private Dictionary<string, int> interactionCounts = new Dictionary<string, int>();
    
    public Dictionary<string, string> allInteractiveObjects = new Dictionary<string, string>()
    {
        {"Laundry", "Do the Laundry"},
        {"Roof Garden", "Water the Roof Garden"},
        {"E-Waste", "Dispose of E-Waste"},
        {"Pill", "Take Medicine"},
        {"Pan", "Use the Pan"},
        {"Fridge", "Check the Fridge"},
        {"Laundry_Second", "Check the Laundry Again"}
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
        // Track regular interactions
        interactedObjects.Add(objectTag);
        
        // Track interaction count
        if (interactionCounts.ContainsKey(objectTag))
        {
            interactionCounts[objectTag]++;
        }
        else
        {
            interactionCounts[objectTag] = 1;
        }
        
        Debug.Log($"Marked {objectTag} as interacted (Count: {interactionCounts[objectTag]})");
        
        // Note: Laundry_Second completion is now handled by TimeBasedInteraction script
        // based on different demand levels, not just interaction count
    }
    
    public bool HasInteracted(string objectTag)
    {
        return interactedObjects.Contains(objectTag);
    }
    
    public int GetInteractionCount(string objectTag)
    {
        return interactionCounts.ContainsKey(objectTag) ? interactionCounts[objectTag] : 0;
    }
    
    public bool HasInteractedMultipleTimes(string objectTag, int requiredCount)
    {
        return GetInteractionCount(objectTag) >= requiredCount;
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
        interactionCounts.Clear();
    }
}