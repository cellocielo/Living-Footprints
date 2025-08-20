using UnityEngine;
using System.Collections;

public class PlayerPositionRestorer : MonoBehaviour
{
    public Transform player;
    
    void Start()
    {
        if (SceneStateManager.hasStoredPosition)
        {
            StartCoroutine(RestorePlayerPositionDelayed());
        }
    }

    IEnumerator RestorePlayerPositionDelayed()
    {
        // Wait for end of frame to ensure all Start() methods have been called
        yield return new WaitForEndOfFrame();
        
        // Find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.Find("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        if (player != null)
        {
            player.transform.position = SceneStateManager.savedPlayerPosition;
            Debug.Log($"Restored player position to {SceneStateManager.savedPlayerPosition}");
            
            // Also handle camera position if stored
            if (SceneStateManager.hasSavedCameraPosition)
            {
                Camera.main.transform.position = SceneStateManager.savedCameraPosition;
                Debug.Log($"Restored camera position to {SceneStateManager.savedCameraPosition}");
            }
        }
        else
        {
            Debug.LogError("Player transform not found for position restoration!");
        }

        // Reset flags
        SceneStateManager.hasStoredPosition = false;
        SceneStateManager.hasSavedCameraPosition = false;
    }
}