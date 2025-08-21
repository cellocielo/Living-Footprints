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
        yield return new WaitForEndOfFrame();
        
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

        SceneStateManager.hasStoredPosition = false;
        SceneStateManager.hasSavedCameraPosition = false;
    }
}