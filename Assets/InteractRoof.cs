using UnityEngine;

public class InteractRoof : MonoBehaviour
{
    public GameObject promptUI;
    public Vector3 teleportPosition;
    public Vector3 teleportRotation;
    public GameObject player;

    private bool isPlayerNear = false;

    void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered roof teleport pad");
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
            if (promptUI != null)
                promptUI.SetActive(false);

            if (player != null)
            {
                player.transform.position = teleportPosition;
                player.transform.eulerAngles = teleportRotation;
            }
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

        // Debug logging to see which UI is active
        if (roofUIActive)
            Debug.Log("Go To Roof UI is active");
        if (apartmentUIActive)
            Debug.Log("Go To Apartment UI is active");

        // Don't block teleportation - let the teleporter work regardless
        // The UI being active shouldn't prevent using the teleporter
        return false;
    }
}