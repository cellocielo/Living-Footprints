using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractPromptTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public GameObject promptUI;
    public Checklist Checklist;
    public DigitalClock DigitalClock;

    private bool isPlayerNear = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);

        if (Checklist == null)
        {
            Checklist = GameObject.Find("Checklist")?.GetComponent<Checklist>();
            if (Checklist == null)
            {
                Debug.LogError("Checklist Controller not found or Checklist script missing.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("on trigger enter");
            if (promptUI != null && !IsAnyUIActive())
                promptUI.SetActive(true);

            Debug.Log($"Player near {gameObject.name} - Press E to interact");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !IsAnyUIActive() && !IsBlockingUIActive())
        {
            SetSceneUIFlag();
            MarkObjectAsInteracted();
            SceneStateManager.StorePlayerPosition();
            FreeCursorForNewScene();
            if (SceneManager.GetActiveScene().name == "ApartmentScene")
            {
                DigitalClock.Instance.SaveTimeBeforeSceneChange();
            }
            SceneManager.LoadScene(sceneToLoad);
            promptUI.SetActive(false);
        }
    }
    
    private void FreeCursorForNewScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Debug.Log("Cursor freed for new scene - ready for UI interaction");
    }

    private void MarkObjectAsInteracted()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string objectTag = gameObject.tag;
        
        if (currentSceneName == "HouseScene")
        {
            if (InteractionTrackerHouse.Instance != null)
            {
                InteractionTrackerHouse.Instance.MarkAsInteracted(objectTag);
            }
        }
        else if (currentSceneName == "ApartmentScene")
        {
            if (InteractionTrackerApartment.Instance != null)
            {
                InteractionTrackerApartment.Instance.MarkAsInteracted(objectTag);

                if (objectTag == "Laundry")
                {
                    TimeBasedInteraction timeBasedScript = FindObjectOfType<TimeBasedInteraction>();
                    if (timeBasedScript != null)
                    {
                        timeBasedScript.OnLaundryInteraction();
                    }
                    else
                    {
                        Debug.LogWarning("TimeBasedInteraction script not found in scene!");
                    }
                }
            }
        }
    }

    private void SetSceneUIFlag()
    {
        SceneStateManager.ResetFlags();

        if (gameObject.CompareTag("Plants"))
        {
            SceneStateManager.showPlantsUI = true;
        }
        else if (gameObject.CompareTag("Fridge"))
        {
            SceneStateManager.showFridgeUI = true;
        }
        else if (gameObject.CompareTag("PS4"))
        {
            SceneStateManager.showPS4UI = true;
        }
        else if (gameObject.CompareTag("Pill"))
        {
            SceneStateManager.showPillUI = true;
        }
        else if (gameObject.CompareTag("Laundry"))
        {
            SceneStateManager.showLaundryUI = true;
        }
        else if (gameObject.CompareTag("Tree"))
        {
            SceneStateManager.showTreeUI = true;
        }
        else if (gameObject.CompareTag("Rain Barrel"))
        {
            SceneStateManager.showRainBarrelUI = true;
        }
        else if (gameObject.CompareTag("Solar Panel"))
        {
            SceneStateManager.showSolarPanelUI = true;
        }
        else if (gameObject.CompareTag("Pizza Box"))
        {
            SceneStateManager.showPizzaBoxUI = true;
        }
        else if (gameObject.CompareTag("E-Waste"))
        {
            SceneStateManager.showEWasteUI = true;
        }
        else if (gameObject.CompareTag("Pan"))
        {
            SceneStateManager.showPanUI = true;
        }
        else if (gameObject.CompareTag("Car"))
        {
            SceneStateManager.showCarUI = true;
        }
        else if (gameObject.CompareTag("Garden Plant"))
        {
            SceneStateManager.showNativePlantUI = true;
        }
        else if (gameObject.CompareTag("Jam Jar"))
        {
            SceneStateManager.showJamUI = true;
        }
    }
    
    private bool IsAnyUIActive()
    {
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UIPopUp");
        foreach (GameObject ui in uiObjects)
        {
            if (ui.activeInHierarchy)
                return true;
        }
        return false;
    }

    private bool IsBlockingUIActive()
    {
        GameObject goToRoof = GameObject.Find("Go To Roof");
        GameObject goToApartment = GameObject.Find("Go To Apartment");

        bool roofUIActive = goToRoof != null && goToRoof.activeInHierarchy;
        bool apartmentUIActive = goToApartment != null && goToApartment.activeInHierarchy;

        return roofUIActive || apartmentUIActive;
    }
}