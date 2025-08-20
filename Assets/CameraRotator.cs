using UnityEngine;
using Unity.Cinemachine;

public class CameraOrbitInput : MonoBehaviour
{
    public CinemachineCamera cineCam;
    public float horizontalSpeed = 100f;
    public float verticalSpeed = 2f;

    private CinemachineOrbitalFollow orbitalFollow;

    void Start()
    {
        orbitalFollow = cineCam.GetComponent<CinemachineOrbitalFollow>();
    }

    void Update()
    {
        if (orbitalFollow != null)
        {
            float horizontalInput = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
            float verticalInput = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

            orbitalFollow.HorizontalAxis.Value += horizontalInput;
            orbitalFollow.VerticalAxis.Value -= verticalInput;
        }
    }
}
