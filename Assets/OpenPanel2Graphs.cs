using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenPanel2Graphs : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject graph1Panel;
    public GameObject graph2Panel;
    public GameObject button2;
    public GameObject opengraph1;
    public GameObject opengraph2;
    public Button graph1Close;
    public Button graph2Close;

    void Start() {
        graph1Close.onClick.AddListener(ShowPopup);
        graph2Close.onClick.AddListener(ShowPopup);
    }

    public void ShowPopup() {
        Debug.Log("Showing popup");
        popupPanel.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        opengraph1.gameObject.SetActive(true);
        opengraph2.gameObject.SetActive(true);
        graph1Panel.gameObject.SetActive(false);
        graph1Close.gameObject.SetActive(false);
        graph2Panel.gameObject.SetActive(false);
        graph2Close.gameObject.SetActive(false);
    }
}
