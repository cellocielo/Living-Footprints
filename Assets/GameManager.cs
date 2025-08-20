using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public static Vector3 spawnPosition = Vector3.zero;
    
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetHouseSpawn() {
        spawnPosition = new Vector3(14, -16, -11);
        Debug.Log("HOUSE: Setting spawn position to: " + spawnPosition);
        SceneManager.LoadScene("HouseScene");
    }
    
    public void SetApartmentSpawn() {
        spawnPosition = new Vector3(-12, 1, 3);
        Debug.Log("APARTMENT: Setting spawn position to: " + spawnPosition);
        SceneManager.LoadScene("ApartmentScene");
    }
}