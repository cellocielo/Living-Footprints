using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Existing UI Elements")]
    public GameObject housePlay;
    public GameObject apartmentPlay;
    public GameObject houseBorder;
    public GameObject apartmentBorder;
    public GameObject apartmentButton;
    public GameObject houseButton;
    public GameObject howToPlayButton;
    public GameObject title;
    public GameObject options;
    public GameObject closePages;
    public GameObject pages;
    public GameObject left;
    public GameObject right;

    [Header("Star Completion Indicators")]
    public GameObject apartmentStarObject; // Drag your apartment star GameObject here in the inspector
    public GameObject houseStarObject; // Drag your house star GameObject here in the inspector

    void Start()
    {
        UpdateStarVisibility();
    }

    void OnEnable()
    {
        // Update star visibility whenever the menu becomes active
        UpdateStarVisibility();
    }

    public void showApartmentImage()
    {
        housePlay.gameObject.SetActive(false);
        apartmentPlay.gameObject.SetActive(true);
        houseBorder.gameObject.SetActive(false);
        apartmentBorder.gameObject.SetActive(true);
        pages.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    public void showHouseImage()
    {
        apartmentPlay.gameObject.SetActive(false);
        housePlay.gameObject.SetActive(true);
        apartmentBorder.gameObject.SetActive(false);
        houseBorder.gameObject.SetActive(true);
        pages.gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    public void showHowTo()
    {
        apartmentBorder.gameObject.SetActive(false);
        houseBorder.gameObject.SetActive(false);
        housePlay.gameObject.SetActive(false);
        apartmentPlay.gameObject.SetActive(false);
        apartmentButton.gameObject.SetActive(false);
        houseButton.gameObject.SetActive(false);
        howToPlayButton.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        pages.gameObject.SetActive(true);
        closePages.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
    }

    public void closePagePanel()
    {
        pages.gameObject.SetActive(false);
        closePages.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        houseButton.gameObject.SetActive(true);
        apartmentButton.gameObject.SetActive(true);
        howToPlayButton.gameObject.SetActive(true);
        options.gameObject.SetActive(true);

        // Refresh star visibility when returning to main menu
        UpdateStarVisibility();
    }

    // Star functionality methods
    public void UpdateStarVisibility()
    {
        UpdateApartmentStar();
        UpdateHouseStar();
    }

    private void UpdateApartmentStar()
    {
        if (apartmentStarObject != null)
        {
            bool apartmentCompleted = SceneStateManager.IsApartmentCompleted();
            apartmentStarObject.SetActive(apartmentCompleted);

            if (apartmentCompleted)
            {
                Debug.Log("Apartment completed! Showing apartment star.");
            }
            else
            {
                int completed = SceneStateManager.GetCompletedApartmentTaskCount();
                int total = SceneStateManager.GetTotalApartmentTaskCount();
                Debug.Log($"Apartment progress: {completed}/{total} tasks completed.");
            }
        }
        else
        {
            Debug.LogWarning("Apartment star object reference is missing! Please assign it in the inspector.");
        }
    }

    private void UpdateHouseStar()
    {
        if (houseStarObject != null)
        {
            bool houseCompleted = SceneStateManager.IsHouseCompleted();
            houseStarObject.SetActive(houseCompleted);

            if (houseCompleted)
            {
                Debug.Log("House completed! Showing house star.");
            }
            else
            {
                int completed = SceneStateManager.GetCompletedHouseTaskCount();
                int total = SceneStateManager.GetTotalHouseTaskCount();
                Debug.Log($"House progress: {completed}/{total} tasks completed.");
            }
        }
        else
        {
            Debug.LogWarning("House star object reference is missing! Please assign it in the inspector.");
        }
    }

    // Call this method whenever you return to the menu to refresh the stars
    public void RefreshMenu()
    {
        UpdateStarVisibility();
    }
}