using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapePopup : MonoBehaviour 
{
    public GameObject escapePopupPanel;
    public Button yesButton;
    public Button noButton;

    private void Start() 
    {
        if (escapePopupPanel != null)
            escapePopupPanel.SetActive(false);
                     
        if (yesButton != null)
            yesButton.onClick.AddListener(OnYesClicked);
        if (noButton != null)
            noButton.onClick.AddListener(OnNoClicked);
    }

    private void Update() 
    {
        if (SceneManager.GetActiveScene().name != "MenuScene") 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                DisableAllUIPopUps();
                
                if (escapePopupPanel != null)
                    escapePopupPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
            }
        }
    }

    private void DisableAllUIPopUps() {
        GameObject[] uiPopUps = GameObject.FindGameObjectsWithTag("UIPopUp");
        foreach (GameObject popUp in uiPopUps)
        {
            popUp.SetActive(false);
        }
    }

    private void OnYesClicked() {
        SceneManager.LoadScene("Menu");
        SceneStateManager.ResetFlags();
    }

    private void OnNoClicked() {
        if (escapePopupPanel != null)
            escapePopupPanel.SetActive(false);
    }
}