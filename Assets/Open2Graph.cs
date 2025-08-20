using UnityEngine;

public class Open2Graph : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject popupPanel2;
    public GameObject currentPanel;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public void ShowPopup1() {
        currentPanel.SetActive(false);
        popupPanel.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(true);
    }

    public void ShowPopup2() {
        currentPanel.SetActive(false);
        popupPanel2.SetActive(true);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button5.gameObject.SetActive(true);
    }
}
