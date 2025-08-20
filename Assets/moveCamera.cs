using UnityEngine;
using UnityEngine.SceneManagement;

public class moveCamera : MonoBehaviour 
{
    public Vector3 offset = new Vector3(0, 1.8f, 0);

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only set initial position if we're NOT restoring a saved position
        if (!SceneStateManager.hasStoredPosition)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                transform.position = player.transform.position + offset;
            }
        }
        // If we are restoring, let the PlayerPositionRestorer handle it first
    }

    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
        else 
        {
            Debug.Log("Player not found in moveCamera");
        }
    }
}