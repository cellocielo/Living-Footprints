using UnityEngine;
using TMPro;

public class TimeBasedInteraction : MonoBehaviour
{
    public DigitalClock clock;
    public TextMeshProUGUI rateText;

    private static string firstLaundryDemandLevel = null;

    private static bool hasCheckedLaundrySecondTime = false;
    
    public static bool shouldCompleteSecondLaundryTask = false;

    public bool HasCheckedLaundrySecondTime()
    {
        return hasCheckedLaundrySecondTime;
    }


    public string GetTimeBasedMessage()
    {
        int hour = clock.GetCurrentHour();

        if (hour >= 22 || hour < 6)
        {
            return "You are using electricity during periods of low demand! (10PM - 6AM)";
        }
        else if (hour >= 16 && hour < 22)
        {
            return "You are using electricity during periods of high demand! (4PM - 10PM)";
        }
        else
        {
            return "You are using electricity during periods of moderate demand! (6AM - 4PM)";
        }
    }

    public string GetCurrentDemandLevel()
    {
        int hour = clock.GetCurrentHour();

        if (hour >= 22 || hour < 6)
        {
            return "Low";
        }
        else if (hour >= 16 && hour < 22)
        {
            return "High";
        }
        else
        {
            return "Moderate";
        }
    }

    public void OnLaundryInteraction()
    {
        string currentDemandLevel = GetCurrentDemandLevel();
        int laundryInteractionCount = InteractionTrackerApartment.Instance.GetInteractionCount("Laundry");
        
        if (firstLaundryDemandLevel == null) // Changed this condition
        {
            firstLaundryDemandLevel = currentDemandLevel;
            Debug.Log($"First laundry interaction at {currentDemandLevel} demand level - task will complete when popup is closed!");
        }
        else if (!hasCheckedLaundrySecondTime) // Changed this condition too
        {
            Debug.Log($"Second laundry interaction: First was {firstLaundryDemandLevel}, now is {currentDemandLevel}");
            
            if (firstLaundryDemandLevel != currentDemandLevel)
            {
                shouldCompleteSecondLaundryTask = true;
                hasCheckedLaundrySecondTime = true;
                Debug.Log("Different demand level detected - second laundry task will complete when panel closes!");
            }
            else
            {
                Debug.Log("Laundry checked at same demand level - Advanced task not completed yet");
            }
        }
    }
}