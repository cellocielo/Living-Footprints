using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 20f;
    public float distance = 5f;
    public Vector3 offset = Vector3.up;

    void Start()
    {
        transform.position = target.position + offset - transform.forward * distance;
    }

    void Update()
    {
        transform.RotateAround(target.position + offset, Vector3.up, rotationSpeed * Time.deltaTime);
        transform.LookAt(target.position + offset);
    }
}
