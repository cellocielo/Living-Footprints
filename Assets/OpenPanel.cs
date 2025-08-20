using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenPanel : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject graphPanel;
    public Button button1;
    public GameObject button2;
    public GameObject button3;

    void Start() {
        button1.onClick.AddListener(ShowPopup);
    }

    public void ShowPopup() {
        Debug.Log("Showing popup");
        popupPanel.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        graphPanel.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
    }
}
