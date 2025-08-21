using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TaskCompletionPopup : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject popupPanel;
    public Image checkmarkImage;
    public TextMeshProUGUI completionText;
    public Image backgroundOverlay;
    
    [Header("Animation Settings")]
    public float fadeInDuration = 0.5f;
    public float displayDuration = 2.0f;
    public float fadeOutDuration = 0.5f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [Header("Checkmark Settings")]
    public Color checkmarkColor = Color.green;
    public float checkmarkScale = 1.2f;
    
    [Header("Text Size Settings")]
    public float normalFontSize = 24f;
    public float smallFontSize = 18f;
    
    [Header("Text Position Settings")]
    public float normalYOffset = 0f;
    public float smallTextYOffset = 20f;
    
    private static TaskCompletionPopup instance;
    private float originalFontSize;
    private Vector3 originalTextPosition;
    
    public static TaskCompletionPopup Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TaskCompletionPopup>();
                if (instance == null)
                {
                    Debug.LogWarning("TaskCompletionPopup instance not found in scene!");
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializePopup();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void InitializePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
            
        if (checkmarkImage != null)
        {
            checkmarkImage.color = new Color(checkmarkColor.r, checkmarkColor.g, checkmarkColor.b, 0);
        }
        
        if (completionText != null)
        {
            originalFontSize = completionText.fontSize;
            Color textColor = completionText.color;
            completionText.color = new Color(textColor.r, textColor.g, textColor.b, 0);
        }
        
        if (backgroundOverlay != null)
        {
            backgroundOverlay.color = new Color(0, 0, 0, 0);
        }
    }
    
    public static void ShowTaskCompletion(string taskName = "Task Completed!")
    {
        Debug.Log("task name: " + taskName);
        if (Instance != null)
        {
            Instance.StartCoroutine(Instance.ShowCompletionAnimation(taskName));
        }
    }
    
    public static void ShowTaskCompletionWithType(string taskName, string taskType)
    {
        Debug.Log("task name: " + taskName + ", task type: " + taskType);
        if (Instance != null)
        {
            Instance.StartCoroutine(Instance.ShowCompletionAnimationWithType(taskName, taskType));
        }
    }
    
    private IEnumerator ShowCompletionAnimation(string taskName)
    {
        return ShowCompletionAnimationWithType(taskName, "");
    }
    
    private IEnumerator ShowCompletionAnimationWithType(string taskName, string taskType)
    {
        Debug.Log("Show Completion Animation Called");
        Debug.Log("taskName parameter: '" + taskName + "'");
        Debug.Log("taskType parameter: '" + taskType + "'");
        Debug.Log("taskType.ToLower(): '" + taskType.ToLower() + "'");
        Debug.Log("Is laundry_second? " + (taskType.ToLower() == "laundry_second"));
        Debug.Log("Completion Text component: " + completionText);
        Debug.Log("completionText.gameObject: " + completionText.gameObject.name);
        Debug.Log("completionText.text BEFORE: '" + completionText.text + "'");
        
        if (popupPanel != null)
            popupPanel.SetActive(true);
        
        yield return null;
        
        if (completionText != null)
        {
            originalTextPosition = completionText.rectTransform.anchoredPosition;
            Debug.Log($"Captured original position: {originalTextPosition}");
            Debug.Log($"RectTransform info - Anchors: {completionText.rectTransform.anchorMin} to {completionText.rectTransform.anchorMax}");
            Debug.Log($"RectTransform info - Pivot: {completionText.rectTransform.pivot}");
            
            completionText.text = taskName;
            
            Debug.Log($"About to check taskType. Value: '{taskType}', Length: {taskType.Length}");
            Debug.Log($"taskType.ToLower(): '{taskType.ToLower()}'");
            Debug.Log($"Comparison result: {taskType.ToLower() == "laundry_second"}");
            
            if (taskType.ToLower() == "laundry_second")
            {
                completionText.fontSize = smallFontSize;
                Debug.Log($"LAUNDRY_SECOND: Applying offset {smallTextYOffset}");
                
                Vector2 newPosition = originalTextPosition;
                newPosition.y = originalTextPosition.y + smallTextYOffset;
                
                completionText.rectTransform.anchoredPosition = newPosition;
                Debug.Log($"Position set to: {completionText.rectTransform.anchoredPosition}");
                
                LayoutRebuilder.ForceRebuildLayoutImmediate(completionText.rectTransform);
                
                Vector3 localPos = completionText.rectTransform.localPosition;
                localPos.y = newPosition.y;
                completionText.rectTransform.localPosition = localPos;
                
                Debug.Log($"Final anchoredPosition: {completionText.rectTransform.anchoredPosition}");
                Debug.Log($"Final localPosition: {completionText.rectTransform.localPosition}");
            }
            else
            {
                completionText.fontSize = normalFontSize;
                Vector2 newPosition = originalTextPosition;
                newPosition.y = originalTextPosition.y + normalYOffset;
                completionText.rectTransform.anchoredPosition = newPosition;
                Debug.Log($"NORMAL: Position set to: {completionText.rectTransform.anchoredPosition}");
            }
        }
        
        Debug.Log("Completion Text 2: " + completionText);
        
        yield return StartCoroutine(FadeInBackground());
        
        yield return StartCoroutine(AnimateCheckmark());
        
        yield return StartCoroutine(FadeInText());
        
        yield return new WaitForSeconds(displayDuration);
        
        yield return StartCoroutine(FadeOutPopup());
        
        if (completionText != null)
        {
            completionText.fontSize = originalFontSize;
            completionText.rectTransform.anchoredPosition = originalTextPosition;
        }
        
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
    
    private IEnumerator FadeInBackground()
    {
        if (backgroundOverlay == null) yield break;
        
        float elapsedTime = 0;
        Color startColor = new Color(0, 0, 0, 0);
        Color endColor = new Color(0, 0, 0, 0.5f);
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            backgroundOverlay.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        backgroundOverlay.color = endColor;
    }
    
    private IEnumerator AnimateCheckmark()
    {
        if (checkmarkImage == null) yield break;
        
        float elapsedTime = 0;
        Color startColor = new Color(checkmarkColor.r, checkmarkColor.g, checkmarkColor.b, 0);
        Color endColor = checkmarkColor;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            
            float scaleValue = scaleCurve.Evaluate(t) * checkmarkScale;
            checkmarkImage.transform.localScale = Vector3.one * scaleValue;
            
            checkmarkImage.color = Color.Lerp(startColor, endColor, t);
            
            yield return null;
        }
        
        checkmarkImage.transform.localScale = Vector3.one * checkmarkScale;
        checkmarkImage.color = endColor;
        
        yield return StartCoroutine(BounceToNormalSize());
    }
    
    private IEnumerator BounceToNormalSize()
    {
        if (checkmarkImage == null) yield break;
        
        float bounceTime = 0.2f;
        float elapsedTime = 0;
        Vector3 startScale = Vector3.one * checkmarkScale;
        Vector3 endScale = Vector3.one * .5f;
        
        while (elapsedTime < bounceTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / bounceTime;
            checkmarkImage.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        
        checkmarkImage.transform.localScale = endScale;
    }
    
    private IEnumerator FadeInText()
    {
        if (completionText == null) yield break;
        
        float elapsedTime = 0;
        Color startColor = new Color(completionText.color.r, completionText.color.g, completionText.color.b, 0);
        Color endColor = new Color(completionText.color.r, completionText.color.g, completionText.color.b, 1);
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            completionText.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        completionText.color = endColor;
    }
    
    private IEnumerator FadeOutPopup()
    {
        float elapsedTime = 0;
        
        Color checkmarkStart = checkmarkImage != null ? checkmarkImage.color : Color.clear;
        Color textStart = completionText != null ? completionText.color : Color.clear;
        Color backgroundStart = backgroundOverlay != null ? backgroundOverlay.color : Color.clear;
        
        Color checkmarkEnd = new Color(checkmarkStart.r, checkmarkStart.g, checkmarkStart.b, 0);
        Color textEnd = new Color(textStart.r, textStart.g, textStart.b, 0);
        Color backgroundEnd = new Color(backgroundStart.r, backgroundStart.g, backgroundStart.b, 0);
        
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeOutDuration;
            
            if (checkmarkImage != null)
                checkmarkImage.color = Color.Lerp(checkmarkStart, checkmarkEnd, t);
            
            if (completionText != null)
                completionText.color = Color.Lerp(textStart, textEnd, t);
            
            if (backgroundOverlay != null)
                backgroundOverlay.color = Color.Lerp(backgroundStart, backgroundEnd, t);
            
            yield return null;
        }
        
        // Ensure final state
        if (checkmarkImage != null)
            checkmarkImage.color = checkmarkEnd;
        if (completionText != null)
            completionText.color = textEnd;
        if (backgroundOverlay != null)
            backgroundOverlay.color = backgroundEnd;
    }
}