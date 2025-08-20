using UnityEngine;

public class OpenGraph : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject currentPanel;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    public void ShowPopup() {
        currentPanel.SetActive(false);
        popupPanel.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(true);
    }
}
