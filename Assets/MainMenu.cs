using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton;

    void Start() {
        playButton.onClick.AddListener(PlayGame);
    }

    void PlayGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
