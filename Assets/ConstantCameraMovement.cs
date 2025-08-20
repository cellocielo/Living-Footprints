using UnityEngine;

public class ConstantCameraMovement : MonoBehaviour 
{
    public float speed = 5f;
    public Vector3 direction = Vector3.forward;
    
    void Update() 
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}