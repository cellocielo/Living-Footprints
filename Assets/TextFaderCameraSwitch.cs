using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextFaderCameraSwitch : MonoBehaviour
{
    [Header("Text Stuff")]
    public TextMeshProUGUI textComponent;
    public string[] messages;
    public float fadeDuration = 1f;
    public float displayTime = 4f;

    [Header("Cam Settings")]
    public int switchCamAt = 2;
    public Vector3 camPos = new Vector3(14.61f, 0.7f, -50.0f);
    public Vector3 camRot = new Vector3(0f, 210f, 0f);

    [Header("Screen Fade")]
    public Image screenFader;

    private void Start()
    {
        StartCoroutine(CycleText());
    }

    IEnumerator CycleText()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            if (i == switchCamAt)
            {
                yield return StartCoroutine(FadeScreenAlpha(0f, 1f));
                MoveCamera();
                yield return StartCoroutine(FadeScreenAlpha(1f, 0f));
            }

            textComponent.text = messages[i];

            yield return StartCoroutine(FadeTextAlpha(0, 1));
            yield return new WaitForSeconds(displayTime);
            yield return StartCoroutine(FadeTextAlpha(1, 0));
        }

        textComponent.text = "";
        
        string currentScene = SceneManager.GetActiveScene().name;
        string targetScene = GetTargetSceneFromCurrent(currentScene);
        SceneManager.LoadScene(targetScene);
    }

    private string GetTargetSceneFromCurrent(string currentScene)
    {
        if (SceneManager.GetActiveScene().name == "ApartmentScene") {
        DigitalClock.Instance.gameObject.SetActive(true);
        }
        switch (currentScene)
        {
            case "PhantomLoadScene":
            case "PizzaBoxScene":
            case "TreeScene":
            case "RainBarrelScene":
            case "CarScene":
            case "JamScene":
            case "NativePlantScene":
                return "HouseScene";
                
            case "FridgeScene":
            case "LaundryScene":
            case "PanScene":
            case "PillScene":
            case "EWasteScene":
            case "PlantsScene":
            case "SolarPanelScene":
                return "ApartmentScene";
                
            default:
                return "SampleScene";
        }
    }

    IEnumerator FadeTextAlpha(float from, float to)
    {
        float t = 0f;
        Color c = textComponent.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            textComponent.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        textComponent.color = new Color(c.r, c.g, c.b, to);
    }

    IEnumerator FadeScreenAlpha(float from, float to)
    {
        float t = 0f;
        Color c = screenFader.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            screenFader.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        screenFader.color = new Color(c.r, c.g, c.b, to);
    }

    void MoveCamera()
    {
        var cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("Main Camera not found.");
            return;
        }

        cam.transform.position = camPos;
        cam.transform.rotation = Quaternion.Euler(camRot);
    }
}
