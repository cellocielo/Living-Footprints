using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CompletionPopUp : MonoBehaviour
{
    [Header("UI Elements - Assign in Inspector")]
    public GameObject popupPanel;
    public TextMeshProUGUI completionText;
    public Button yesButton;
    public Button noButton;
    public Image backgroundOverlay;
    
    [Header("Animation Settings")]
    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;
    
    [Header("Text Settings")]
    public float completionTextFontSize = 18f;
    
    private static CompletionPopUp instance;
    
    public static CompletionPopUp Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CompletionPopUp>();
            }
            return instance;
        }
    }
    
    private void Start()
    {
        instance = this;
        
        if (popupPanel != null)
            popupPanel.SetActive(false);
            
        if (yesButton != null)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(OnYesClicked);
        }
            
        if (noButton != null)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(OnNoClicked);
        }
    }
    
    public static void ShowCompletionPopUp(bool isApartment)
    {
        if (Instance != null)
        {
            string message = isApartment ? 
                "Congratulations! You've completed all apartment tasks!\nWould you like to exit to the main menu?" :
                "Congratulations! You've completed all house tasks!\nWould you like to exit to the main menu?";
            
            Instance.StartCoroutine(Instance.ShowPopupAnimation(message));
        }
        else
        {
            Debug.LogError("CompletionPopUp instance not found! Make sure you have a CompletionPopUp component in the scene.");
        }
    }
    
    private IEnumerator ShowPopupAnimation(string message)
    {
        // Check if all required components are assigned
        if (popupPanel == null || completionText == null || yesButton == null || noButton == null)
        {
            Debug.LogError("CompletionPopUp: Missing UI component assignments in Inspector!");
            Debug.LogError($"Panel: {popupPanel != null}, Text: {completionText != null}, YesBtn: {yesButton != null}, NoBtn: {noButton != null}");
            yield break;
        }
        
        popupPanel.SetActive(true);
        completionText.text = message;
        
        // Set the font size to be smaller
        if (completionText != null)
        {
            completionText.fontSize = completionTextFontSize;
        }
        
        // Fade in background
        yield return StartCoroutine(FadeInBackground());
        
        // Fade in popup elements
        yield return StartCoroutine(FadeInPopup());
    }
    
    private IEnumerator FadeInBackground()
    {
        if (backgroundOverlay == null) yield break;
        
        float elapsedTime = 0;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(0, 0, 0, 0.7f);
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            backgroundOverlay.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        backgroundOverlay.color = endColor;
    }
    
    private IEnumerator FadeInPopup()
    {
        if (completionText == null) yield break;
        
        float elapsedTime = 0;
        
        // Set initial alpha to 0
        SetUIElementsAlpha(0f);
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            
            SetUIElementsAlpha(t);
            
            yield return null;
        }
        
        SetUIElementsAlpha(1f);
    }
    
    private void SetUIElementsAlpha(float alpha)
    {
        if (completionText != null)
        {
            Color textColor = completionText.color;
            completionText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
        }
        
        if (yesButton != null)
        {
            Image buttonImage = yesButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color buttonColor = buttonImage.color;
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);
            }
            
            TextMeshProUGUI buttonText = yesButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                Color textColor = buttonText.color;
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            }
        }
        
        if (noButton != null)
        {
            Image buttonImage = noButton.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color buttonColor = buttonImage.color;
                buttonImage.color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, alpha);
            }
            
            TextMeshProUGUI buttonText = noButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                Color textColor = buttonText.color;
                buttonText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            }
        }
    }
    
    private void OnYesClicked()
    {
        SceneManager.LoadScene("Menu");
        SceneStateManager.ResetFlags();
    }
    
    private void OnNoClicked()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
    
    private IEnumerator FadeOutPopup()
    {
        float elapsedTime = 0;
        
        Color backgroundStart = backgroundOverlay != null ? backgroundOverlay.color : Color.clear;
        Color backgroundEnd = new Color(backgroundStart.r, backgroundStart.g, backgroundStart.b, 0);
        
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeOutDuration;
            
            if (backgroundOverlay != null)
                backgroundOverlay.color = Color.Lerp(backgroundStart, backgroundEnd, t);
                
            SetUIElementsAlpha(1f - t);
            
            yield return null;
        }
        
        if (backgroundOverlay != null)
            backgroundOverlay.color = backgroundEnd;
            
        SetUIElementsAlpha(0f);
    }
}